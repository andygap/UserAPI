using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public AuthController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Autenticar o usuario
        /// </summary>

        [Route("login")]
        [HttpPost]
        public IActionResult Login()
        {
            return Ok();
        }
        /// <summary>
        ///  Reiniciar senha de acesso do usuario
        /// </summary>
        
        [Route("reset-password")]
        [HttpPost]
        public IActionResult ResetPassword()
        {
            return Ok();
        }
        /// <summary>
        /// Recuperar senha de acesso do usurio
        /// </summary>
        [Authorize]
        [Route("forgot-password")]
        [HttpPost]
        public IActionResult ForgotPassword()
        {
            return Ok();
        }
    }
}
