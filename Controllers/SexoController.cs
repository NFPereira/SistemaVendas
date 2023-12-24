using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SexoController : ControllerBase
    {
        private SexoRepository sexoRepository;

        public SexoController()
        {
            sexoRepository = new SexoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(sexoRepository.GetSexos());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(sexoRepository.GetSexoById(id));
        }

        [HttpPost]
        public IActionResult Post(SexoModel sexoModel)
        {
            return Ok(sexoRepository.PostSexo(sexoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(SexoModel sexoModel, int id)
        {
            return Ok(sexoRepository.PutSexo(sexoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(sexoRepository.DeleteSexo(id));
        }
    }
}
