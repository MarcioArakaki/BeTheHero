using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BeTheHero.Persitence.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BeTheHero.Persitence
{
    public interface IIncidentRepository
    {
        void Create();
        void Delete();
        IEnumerable<Incident> Get();
        void Update();

    }
}