using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NivelUsuarioController : ControllerBase
    {
        private NivelUsuarioRepository nivelUsuarioRepository;

        public NivelUsuarioController()
        {
            nivelUsuarioRepository = new NivelUsuarioRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(nivelUsuarioRepository.GetNivelUsuario());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(nivelUsuarioRepository.GetNivelUsuarioById(id));
        }
        [HttpPost]
        public IActionResult Post(NivelUsuarioModel nivelUsuarioModel)
        {
            return Ok(nivelUsuarioRepository.PostNivelUsuario(nivelUsuarioModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(NivelUsuarioModel nivelUsuarioModel, int id)
        {
            return Ok(nivelUsuarioRepository.PutNivelUsuario(nivelUsuarioModel, id));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(nivelUsuarioRepository.DeleteNivelUsuario(id));
        }
    }
}
