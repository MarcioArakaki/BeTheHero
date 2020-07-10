using BeTheHero.Persitence;
using BeTheHero.Persitence.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeTheHero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        IIncidentRepository incidentRepository;
        public IncidentController(IIncidentRepository incidentRepository)
        {
            this.incidentRepository = incidentRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var incidents = this.incidentRepository.Get(id);
            return Ok(incidents);
        }

        [HttpGet]
        public IActionResult Get()
        {
            var incidents = this.incidentRepository.Get();
            return Ok(incidents);
        }

        [HttpPost]
        public IActionResult Create(Incident incident)
        {
            this.incidentRepository.Create(incident);
            return Ok("Created");
        }

        [HttpPatch]
        public IActionResult Update(Incident incident)
        {
            this.incidentRepository.Update(incident);
            return Ok("created");
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            this.incidentRepository.Delete(id);
            return Ok("Removed");
        }

        [HttpGet("Index/{page}")]
        public IActionResult Index(int page)
        {
            var pageSize = 1;
            var incidents = this.incidentRepository.GetIncidentsOngPaginated(pageSize,page);
            return Ok(incidents);
        }      
    }
}