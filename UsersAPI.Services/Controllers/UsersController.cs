using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UsersAPI.Services.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Criar conta de usuario
        /// </summary>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add()
        {
            return Ok();
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
