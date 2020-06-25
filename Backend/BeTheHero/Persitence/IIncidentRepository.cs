using System.Collections.Generic;
using BeTheHero.Persitence.Models;

namespace BeTheHero.Persitence
{
    public interface IIncidentRepository 
    {
        void Create(Incident incident);
        void Delete(int id);
        IEnumerable<Incident> Get();
        Incident Get(int id);
        void Update(Incident incident);
    }
}