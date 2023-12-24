using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificacaoFiscalController : ControllerBase
    {
        private ClassificacaoFiscalRepository classificacaoFiscalRepository;

        public ClassificacaoFiscalController()
        {
            classificacaoFiscalRepository = new ClassificacaoFiscalRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(classificacaoFiscalRepository.GetClassificacaoFiscal());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(classificacaoFiscalRepository.GetClassificacaoFiscalById(id));

        }
        [HttpPost]
        public IActionResult Post(ClassificacaoFiscalModel classificacaoFiscalModel)
        {
            return Ok(classificacaoFiscalRepository.PostClassificacaoFisccal(classificacaoFiscalModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(ClassificacaoFiscalModel classificacaoFiscalModel, int id)
        {
            return Ok(classificacaoFiscalRepository.PutClassificacaoFiscal(classificacaoFiscalModel, id));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok(classificacaoFiscalRepository.DeleteClassificacaoFiscal(id));
        }
    }
}
