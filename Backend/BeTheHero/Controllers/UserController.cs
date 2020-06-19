using BeTheHero.Persitence;
using BeTheHero.Persitence.Models;
using Microsoft.AspNetCore.Mvc;

namespace BeTheHero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

        IIncidentRepository incidentRepository;

        public UserController(IIncidentRepository incidentRepository)
        {
            this.incidentRepository = incidentRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(new User
        {
            Id = 1,
            Name = "Arakaki",
            Event = "Be the hero"
        });

        [HttpGet]
        public IActionResult Get()
        {
            var incidents = this.incidentRepository.Get();
            return Ok(incidents);
        }
    }
}