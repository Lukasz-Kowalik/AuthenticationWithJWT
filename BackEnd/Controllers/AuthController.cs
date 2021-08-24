using BackEnd.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtManager _manager;

        public AuthController(IJwtManager manager)
        {
            _manager = manager;
        }

        [HttpPost(template: "SignIn")]
        public async Task<IActionResult> SignInAsync(string email, string password)
        {
            var response = await _manager.SignInAsync(email, password);
            return Ok(response);
        }

        [HttpPost("Refresh")]
        public IActionResult Refresh(string token, string refreshToken)
        {
            var response = _manager.RefreshToken(token, refreshToken);
            return Ok(response);
        }
    }
}