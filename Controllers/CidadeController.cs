using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        private CidadeRepository cidadeRepository;

        public CidadeController()
        {
            cidadeRepository = new CidadeRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(cidadeRepository.GetCidades());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(cidadeRepository.GetCidadeById(id));
        }

        [HttpPost]
        public IActionResult Post(CidadeModel cidadeModel)
        {
            return Ok(Post(cidadeModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(CidadeModel cidadeModel, int id)
        {
            return Ok(cidadeRepository.PutCidade(cidadeModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(cidadeRepository.DeleteCidade(id));
        }
    }
}
