using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormaPagamentoController : ControllerBase
    {
        private FormaPagamentoRepository  formaPagamentoRepository;
        public FormaPagamentoController() {
            formaPagamentoRepository = new FormaPagamentoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(formaPagamentoRepository.GetFormaPagamento());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(formaPagamentoRepository.GetFormaPagamentoById(id));
        }

        [HttpPost]
        public IActionResult Post(FormaPagamentoModel formaPagamentoModel)
        {
            return Ok(formaPagamentoRepository.PostFormaPagamento(formaPagamentoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(FormaPagamentoModel formaPagamentoModel, int id)
        {
            return Ok(formaPagamentoRepository.PutFormaPagamento(formaPagamentoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(formaPagamentoRepository.DeleteFormaPagamento(id));
        }
    }
}
