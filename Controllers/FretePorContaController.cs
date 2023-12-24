using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FretePorContaController : ControllerBase
    {
        private FretePorContaRepository fretePorContaRepository;

        public FretePorContaController()
        {
            fretePorContaRepository = new FretePorContaRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(fretePorContaRepository.GetFretePorConta());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(fretePorContaRepository.GetFretePorContaById(id));
        }
        [HttpPost]
        public IActionResult Post(FretePorContaModel fretePorContaModel)
        {
            return Ok(fretePorContaRepository.PostFretePorConta(fretePorContaModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(FretePorContaModel fretePorContaModel, int id)
        {
            return Ok(fretePorContaRepository.PutFretePorConta(fretePorContaModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(fretePorContaRepository.DeleteFretePorConta(id));
        }

    }
}
