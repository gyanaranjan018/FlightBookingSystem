using AuthService.Models.DTO;
using AuthService.Services;
using Microsoft.AspNetCore.Mvc;
using Utility.Exceptions;

namespace AuthService.Controllers
{

    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager authManager;

        public AuthController(IAuthManager authManager)
        {
            this.authManager = authManager;
        }


        [HttpPost("api/admin/login")]
        public IActionResult AdminLogin(AuthRequest user)
        {
            ModelState.Validate();
            return Ok(authManager.AuthenticateAdmin(user));
        }

        [HttpPost("api/user/login")]
        public IActionResult UserLogin(AuthRequest user)
        {
            ModelState.Validate();
            return Ok(authManager.AuthenticateUser(user));
        }

        [HttpPost("api/user/register")]
        public IActionResult UserRegister(AuthRequest user)
        {
            ModelState.Validate();
            authManager.UserRegister(user);
            return Ok();
        }
    }
}
