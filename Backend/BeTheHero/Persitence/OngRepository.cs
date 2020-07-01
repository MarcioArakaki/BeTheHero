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
    public interface IOngRepository
    {
        void Create(Ong ong);
        void Delete(int id);
        IEnumerable<Ong> Get();
        Ong Get(int id);
        void Update(Ong ong);
    }

    public class OngRepository : IOngRepository
    {
        private readonly string connectionString;
        private readonly IConfiguration Configuration;

        public OngRepository(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.connectionString = this.Configuration["ConnectionStrings:DefaultConnection"];
        }

        public void Create(Ong Ong)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Insert(Ong);
            }
        }

        public void Update(Ong Ong)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Update(Ong);
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Delete(new Ong{ Id = id});
            }
        }

        public IEnumerable<Ong> Get()
        {
            var OngList = new List<Ong>();
            using (var connection = new SqlConnection(connectionString))
            {
                OngList = connection.Query<Ong>("SELECT * FROM Ong", commandTimeout: 2000).ToList();
            }

            return OngList;
        }

        public Ong Get(int id)
        {
            Ong Ong;
            using (var connection = new SqlConnection(connectionString))
            {
                Ong = connection.Get<Ong>(id);
            }

            return Ong;
        }
    }
}