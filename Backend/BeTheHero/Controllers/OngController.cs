using BeTheHero.Persitence;
using BeTheHero.Persitence.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeTheHero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OngController : ControllerBase
    {
        IOngRepository ongRepository;
        public OngController(IOngRepository incidentRepository)
        {
            this.ongRepository = incidentRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var ongs = this.ongRepository.Get(id);
            return Ok(ongs);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var ongs = this.ongRepository.Get();
            return Ok(ongs);
        }

        [HttpPost]
        public IActionResult Create(Ong ong)
        {
            this.ongRepository.Create(ong);
            return Ok("Created");
        }

        [HttpPatch]
        public IActionResult Update(Ong ong)
        {
            this.ongRepository.Update(ong);
            return Ok("created");
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            this.ongRepository.Delete(id);
            return Ok("Removed");
        }
    }
}