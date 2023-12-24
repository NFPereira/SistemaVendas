using api_vendas.Model;
using api_vendas.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api_vendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private PaisRepository paisRepository;

        public PaisController()
        {
            paisRepository = new PaisRepository();
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok (paisRepository.GetPais());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            return Ok(paisRepository.GetPaisById(id));            
        }

        [HttpPost]
        public ActionResult Post(PaisModel paisModel)
        {
            return Ok(paisRepository.PostPais(paisModel));
        }

        [HttpPut("{id}")]
        public ActionResult Put(PaisModel paisModel, int id)
        {
            return Ok(paisRepository.PutPais(paisModel, id));
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            return Ok(paisRepository.DeletePais(id));
        }
    }
}
