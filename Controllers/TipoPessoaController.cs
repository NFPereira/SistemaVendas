using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPessoaController : ControllerBase
    {
        private TipoPessoaRepository tipoPessoaRepository;

        public TipoPessoaController()
        {
            tipoPessoaRepository = new TipoPessoaRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(tipoPessoaRepository.GetTiposPessoa());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(tipoPessoaRepository.GetTipoPessoaById(id));
        }

        [HttpPost]
        public IActionResult Post(TipoPessoaModel tipoPessoaModel)
        {
            return Ok(tipoPessoaRepository.PostTipoPessoa(tipoPessoaModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(TipoPessoaModel tipoPessoaModel, int id) 
        {
            return Ok(tipoPessoaRepository.PutTipoPessoa(tipoPessoaModel, id));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(tipoPessoaRepository.DeleteTipoPessoa(id));
        }
    }
}
