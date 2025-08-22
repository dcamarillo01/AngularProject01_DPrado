using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PL_Angular.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly BL.Usuario _usuario;

        public UsuarioController(BL.Usuario usuario) => _usuario = usuario;


        [HttpPost]
        [Route("Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario) { 
        
            
            var result = _usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else { 
                return BadRequest(result);
            }
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll() { 
            
            var result = _usuario.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else { 
                
                return BadRequest(result);
            }
        }

    }
}
