using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using BeTheHero.Persitence.DTO;
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
                connection.Delete(new Incident { Id = id });
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

        public IncidentDto GetIncidentOng(int id)
        {
            IncidentDto incident;
            using (var connection = new SqlConnection(connectionString))
            {
                incident = connection.QueryFirstOrDefault<IncidentDto>("select I.Id, I.title, I.description, I.value, O.Name as OngName, O.email as OngEmail FROM Incident I INNER JOIN Ong O on I.ongId = O.id");
            }

            return incident;
        }

        public IEnumerable<IncidentDto> GetIncidentsOng()
        {
            IEnumerable<IncidentDto> incidents;
            using (var connection = new SqlConnection(connectionString))
            {
                incidents = connection.Query<IncidentDto>("select I.Id, I.title, I.description, I.value, O.Name as OngName, O.email as OngEmail FROM Incident I INNER JOIN Ong O on I.ongId = O.id");
            }

            return incidents;
        }

        public IEnumerable<IncidentDto> GetIncidentsOngPaginated(int ongId, int pageSize, int pageNumber)
        {
            IEnumerable<IncidentDto> incidents;
            var query = "select I.Id, I.title, I.description, I.value, O.Name as OngName, O.email as OngEmail FROM Incident I INNER JOIN Ong O on I.ongId = O.id WHERE I.ondId = @OngId ORDER BY I.ID ";
            var paginationQuery = "OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var offset = GetOffset(pageSize,pageNumber);
            using (var connection = new SqlConnection(connectionString))
            {
                incidents = connection.Query<IncidentDto>($"{query} {paginationQuery}",new {pageSize, offset, ongId});
            }

            return incidents;
        }

         private static int GetOffset(int pageSize, int pageNumber)
        {
            return (pageNumber - 1) * pageSize;
        }
    }
}