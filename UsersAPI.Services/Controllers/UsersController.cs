using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersAPI.Application.Dtos.Requests;
using UsersAPI.Application.Dtos.Responses;
using UsersAPI.Application.Interfaces.Application;

namespace UsersAPI.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;

        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }

        /// <summary>
        /// Criar conta de usuario
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(UserResponseDto),201)]
        public IActionResult Add([FromBody] UserAddRequestDto dto)
        {
            return StatusCode(201, _userAppService.Add(dto));
        }
        /// <summary>
        /// Atualizar conta de usuario
        /// </summary>

        [HttpPut]
        public IActionResult Update()
        {
            return Ok();
        }
        /// <summary>
        /// Excluir conta de usuario
        /// </summary>

        [HttpDelete]
        public IActionResult Delete()
        {
            return Ok();
        }
        /// <summary>
        /// Consultar dados do usuario
        /// </summary>

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

    }
}
