using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EspeciePagamentoController : ControllerBase
    {
        private EspeciePagamentoRepository especiePagamentoRepository;

        public EspeciePagamentoController()
        {
            especiePagamentoRepository = new EspeciePagamentoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(especiePagamentoRepository.GetEspeciePagamento());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(especiePagamentoRepository.GetEspeciePagamentoById(id));
        }

        [HttpPost]
        public IActionResult Post(EspeciePagamentoModel especiePagamentoModel)
        {
            return Ok(especiePagamentoRepository.PostEspeciePagamento(especiePagamentoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(EspeciePagamentoModel especiePagamentoModel, int id)
        {
            return Ok(especiePagamentoRepository.PutEspeciePagamento(especiePagamentoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(especiePagamentoRepository.DeleteEspeciePagamento(id));
        }

    }
}
