namespace BeTheHero.Persitence.Models
{
    [Dapper.Contrib.Extensions.Table("Incident")]
    public class Incident
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
        public double Value { get; set; }
        public int OngId { get; set; }        
    }
}