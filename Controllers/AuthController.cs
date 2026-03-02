using System.Threading.Tasks;
using GroProduct.Data;
using Microsoft.AspNetCore.Mvc;

namespace GroProduct.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {


        private readonly AppDbContext db;

        // Constructor Injection
        public AuthController(AppDbContext context)
        {
            db = context;
        }

        // GET: api/auth/hello
        [HttpGet("hello")]
        public string HelloMethod()
        {
            return "Hello from Auth";
        }

        // GET: api/auth/login
        [HttpGet("login")]
        public IActionResult Login(string email , string password)
        {
            // You can access HttpContext directly (no need to pass it)
            var request = HttpContext.Request;

            return Ok("Email: " + email + " Password: " + password);
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Models.User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(user);
            await db.SaveChangesAsync();

            // return the created user (or you could return a 201 Created with location)
            return Ok(user);
        }
    }
}