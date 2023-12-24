using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NaturezaController : ControllerBase
    {
        private NaturezaRepository naturezaRepository;

        public NaturezaController()
        {
            naturezaRepository = new NaturezaRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(naturezaRepository.GetNatureza());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            return Ok(naturezaRepository.GetNaturezaById(id));
        }

        [HttpPost]
        public IActionResult Post(NaturezaModel naturezaModel)
        {
            return Ok(naturezaRepository.PostNatureza(naturezaModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(NaturezaModel naturezaModel, int id)
        {
            return Ok(naturezaRepository.PutNatureza(naturezaModel, id));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            return Ok(naturezaRepository.DeleteNatureza(id));
        }
    }
}
