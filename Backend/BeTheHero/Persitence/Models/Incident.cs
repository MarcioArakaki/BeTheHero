namespace BeTheHero.Persitence.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        
        public double Value { get; set; }

        public int OngId { get; set; }
        
    }
}