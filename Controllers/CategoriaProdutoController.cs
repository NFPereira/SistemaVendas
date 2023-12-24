using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaProdutoController : ControllerBase
    {
        private CategoriaProdutoRepository categoriaProdutoRepository;

        public CategoriaProdutoController()
        {
            categoriaProdutoRepository = new CategoriaProdutoRepository();
        }

        [HttpGet]
        public IActionResult GetAll() {
            return Ok(categoriaProdutoRepository.GetCategoriasProdutos());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(categoriaProdutoRepository.GetCategoriasProdutoById(id));
        }

        [HttpPost]
        public IActionResult Post(CategoriaProdutoModel categoriaProdutoModel)
        {
            return Ok(categoriaProdutoRepository.PostCategoriasProduto(categoriaProdutoModel));
        }
        [HttpPut("{id}")]
        public IActionResult Put(CategoriaProdutoModel categoriaProdutoModel, int id)
        {
            return Ok(categoriaProdutoRepository.PutCategoriasProduto(categoriaProdutoModel, id));

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(categoriaProdutoRepository.DeleteCategoriasProduto(id));
        }
    }
}
