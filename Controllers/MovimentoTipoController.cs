using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentoTipoController : ControllerBase
    {
        private MovimentoTipoRepository movimentoTipoRepository;

        public MovimentoTipoController()
        {
            movimentoTipoRepository = new MovimentoTipoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(movimentoTipoRepository.GetMovimentoTipo());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(movimentoTipoRepository.GetMovimentoTipoById(id));
        }

        [HttpPost]
        public IActionResult Post(MovimentoTipoModel movimentoTipoModel)
        {
            return Ok(movimentoTipoRepository.PostMovimentoTipo(movimentoTipoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(MovimentoTipoModel movimentoTipoModel, int id)
        {
            return Ok(movimentoTipoRepository.PutMovimentoTipo(movimentoTipoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(movimentoTipoRepository.DeleteMovimentoTipo(id));
        }
    }
}
