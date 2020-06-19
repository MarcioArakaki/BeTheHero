using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BeTheHero.Persitence.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace BeTheHero.Persitence
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly string connectionString;
        private readonly IConfiguration Configuration;

        public IncidentRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.connectionString = this.Configuration["ConnectionStrings:DefaultConnection"];
        }

        public void Create()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Incident> Get()
        {
            var incidentList = new List<Incident>();
            using (var connection = new SqlConnection(connectionString))
            {
                incidentList = connection.Query<Incident>("SELECT * FROM Incident", commandTimeout: 2000).ToList();
            }

            return incidentList;
        }
    }
}