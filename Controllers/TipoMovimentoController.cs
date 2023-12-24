using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovimentoController : ControllerBase
    {
        private TipoMovimentoRepository tipoMovimentoRepository;

        public TipoMovimentoController()
        {
            tipoMovimentoRepository = new TipoMovimentoRepository();
        }

        [HttpGet]
        public IActionResult GetAll() { 
            return Ok(tipoMovimentoRepository.GetTipoMovimento()); 
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(tipoMovimentoRepository.GetTipoMovimentoById(id));
        }

        [HttpPost]
        public IActionResult Post(TipoMovimentoModel tipoMovimentoModel)
        {
            return Ok(tipoMovimentoRepository.PostTipoMovimento(tipoMovimentoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(TipoMovimentoModel tipoMovimentoModel, int id)
        {
            return Ok(tipoMovimentoRepository.PutTipoMovimento(tipoMovimentoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(tipoMovimentoRepository.DeleteTipoMovimento(id));
        }
    }
}
