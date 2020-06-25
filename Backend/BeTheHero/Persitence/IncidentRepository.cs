using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BeTheHero.Persitence.Models;
using Dapper;
using Dapper.Contrib.Extensions;
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

        public void Create(Incident incident)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Insert(incident);
            }
        }

        public void Update(Incident incident)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Update(incident);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Delete(new Incident{ Id = id});
            }
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

        public Incident Get(int id)
        {
            Incident incident;
            using (var connection = new SqlConnection(connectionString))
            {
                incident = connection.Get<Incident>(id);
            }

            return incident;
        }
    }
}