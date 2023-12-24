using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarcaProdutoController : ControllerBase
    {
        private MarcaProdutoRepository marcaProdutoRepository;

        public MarcaProdutoController()
        {
            marcaProdutoRepository = new MarcaProdutoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(marcaProdutoRepository.GetMarcaProduto());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(marcaProdutoRepository.GetMarcaProdutoById(id));
        }
        [HttpPost]
        public IActionResult Post(MarcaProdutoModel marcaProdutoModel)
        {
            return Ok(marcaProdutoRepository.PostMarcaProduto(marcaProdutoModel));
        }
        [HttpPut("{id}")]
        public IActionResult Put(MarcaProdutoModel marcaProdutoModel, int id)
        {
            return Ok(marcaProdutoRepository.PutMarcaProduto(marcaProdutoModel, id));
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(marcaProdutoRepository.DeleteMarcaProduto(id));
        }
    }
}
