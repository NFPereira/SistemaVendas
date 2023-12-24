using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalArmazenamentoController : ControllerBase
    {
        private LocalArmazenamentoRepository localArmazenamentoRepository;

        public LocalArmazenamentoController()
        {
            localArmazenamentoRepository = new LocalArmazenamentoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(localArmazenamentoRepository.GetLocalArmazenamento());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(localArmazenamentoRepository.GetLocalArmazenamentoById(id));
        }

        [HttpPost]
        public IActionResult Post(LocalArmazenamentoModel localArmazenamentoModel)
        {
            return Ok(localArmazenamentoRepository.PostLocalArmazenamento(localArmazenamentoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(LocalArmazenamentoModel localArmazenamentoModel, int id)
        {
            return Ok(localArmazenamentoRepository.PutLocalArmazenamento(localArmazenamentoModel, id));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(localArmazenamentoRepository.DeleteLocalArmazenamento(id));
        }
    }
}
