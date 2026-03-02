using GroProduct.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GroProduct.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {   
        
        private AppDbContext db_context;
        public UsersController(AppDbContext context) { 
            db_context = context;
        }
        [HttpGet("getall")]
        public IActionResult GetAllUsers()
        {
            return Ok(db_context.Users.ToList());
        }

        [HttpGet("getById/{id}")]
        public  async Task<IActionResult> GetUserById(int id)
        {
            var user = await db_context.Users.FindAsync(id);
            if (user == null)
            {return NotFound("No such user exists");
            }
            else
            {
            return Ok(user);
            }
        }


[HttpPut("updateUserByEmail/{email}")]
    public async Task<IActionResult> UpdateUser(string email, [FromBody] Models.User user)
    {
        var userTemp = await db_context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (userTemp == null)
        {
            return NotFound("No such user exists");
        }

        // Update fields
        userTemp.Name = user.Name;
        userTemp.Email = user.Email;
        userTemp.Password = user.Password;
        userTemp.Phone = user.Phone;

        await db_context.SaveChangesAsync();

        return Ok(userTemp);
    }

    [HttpDelete("deleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await db_context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound("No such user exists");
            }

            db_context.Users.Remove(user);
            await db_context.SaveChangesAsync();

            return Ok("User deleted successfully");
        }


    }
}
