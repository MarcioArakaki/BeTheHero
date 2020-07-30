namespace BeTheHero.Persitence.Models
{
    [Dapper.Contrib.Extensions.Table("Ong")]
    public class Ong
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }

        public string City { get; set; }

        public string Uf { get; set; }
    }
}