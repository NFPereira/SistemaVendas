using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        private EstadoRepository estadoRepository;

        public EstadoController()
        {
            estadoRepository = new EstadoRepository();
        }
        [HttpGet]
        public IActionResult GetAll()
        {            
            return Ok(estadoRepository.GetEstados());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        { 
            return Ok(estadoRepository.GetEstadoById(id));
        }

        [HttpPost]
        public IActionResult Post(EstadoModel estadoModel)
        {
            return Ok(estadoRepository.PostEstado(estadoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(EstadoModel estadoModel, int id)
        {
            return Ok(estadoRepository.PutEstado(estadoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(estadoRepository.DeleteEstado(id));
        }

    }
}
