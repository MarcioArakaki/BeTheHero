using System.Collections.Generic;
using System.Linq;
using BeTheHero.Persitence;
using BeTheHero.Persitence.DTO;
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
            var incident = this.incidentRepository.Get(id);
            return Ok(incident);
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

        [HttpGet("Index/{ongId}/{page}")]
        public IActionResult Index(int page, int ongId)
        {
            var pageSize = 2;
            //  var incidents = this.incidentRepository.GetIncidentsOngPaginated(ongId,pageSize,page);
            var incidents = OutOfMoneyIncidentsOngsRequestTemp();
            Response.Headers.Add("X-Total-Count", incidents.Count().ToString());

            return Ok(incidents);
        }      

        public List<IncidentDto> OutOfMoneyIncidentsOngsRequestTemp ()
        {
            return new List<IncidentDto>(){
                new IncidentDto{
                    Title = "Out of money for azure2",
                    Description = "I ran out of money",
                    Value = 200
                },
                new IncidentDto{
                    Title = "Out of money for azure3",
                    Description = "I ran out of money",
                    Value = 200
                },
                new IncidentDto{
                    Title = "Out of money for azure4",
                    Description = "I ran out of money",
                    Value = 200
                },
                new IncidentDto{
                    Title = "Out of money for azure5",
                    Description = "I ran out of money",
                    Value = 200
                },
                new IncidentDto{
                    Title = "Out of money for azure6",
                    Description = "I ran out of money",
                    Value = 200
                },
                new IncidentDto{
                    Title = "Out of money for azure7",
                    Description = "I ran out of money",
                    Value = 200
                },
            };
        }
    }
}