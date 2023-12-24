using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoProdutoController : ControllerBase
    {
        private GrupoProdutoRepository grupoProdutoRepository;
        public GrupoProdutoController() {
            grupoProdutoRepository = new GrupoProdutoRepository();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(grupoProdutoRepository.GetGrupoProduto());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(grupoProdutoRepository.GetGrupoProdutoById(id));
        }

        [HttpPost]
        public IActionResult Post(GrupoProdutoModel grupoProdutoModel)
        {
            return Ok(grupoProdutoRepository.PostGrupoProduto(grupoProdutoModel));
        }

        [HttpPut("{id}")]
        public IActionResult Put(GrupoProdutoModel grupoProdutoModel, int id)
        {
            return Ok(grupoProdutoRepository.PutGrupoProduto(grupoProdutoModel, id));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(grupoProdutoRepository.DeleteGrupoProduto(id));
        }

    }
}
