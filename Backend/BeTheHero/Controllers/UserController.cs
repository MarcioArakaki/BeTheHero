using Microsoft.AspNetCore.Mvc;

namespace BeTheHero.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(int id) => Ok(new User
        {
            Id = 1,
            Name = "Arakaki",
            Event = "Be the hero"
        });
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Event { get; set; }
    }
}