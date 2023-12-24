using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CorProdutoController : ControllerBase
    {
        private CorProdutoRepository corProdutoRepository;

        public CorProdutoController()
        {
            corProdutoRepository = new CorProdutoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(corProdutoRepository.GetCorProduto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(corProdutoRepository.GetCorProdutoById(id));
        }

        [HttpPost]
        public IActionResult Post(CorProdutoModel corProdutoModel)
        {
            return Ok(corProdutoRepository.PostCorProduto(corProdutoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(CorProdutoModel corProdutoModel, int id)
        {
            return Ok(corProdutoRepository.PutCorProduto(corProdutoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(corProdutoRepository.DeleteCorProduto(id));
        }
    }
}
