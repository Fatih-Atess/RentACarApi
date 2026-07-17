using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RentACarApi.Models;
using RentACarApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RentACarApi.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly string _connectionString;

        public CarRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Car>> GetAvailableCarsAsync()
        {
            using var connection = new MySqlConnection(_connectionString);

            string sql = "SELECT * FROM Cars WHERE Durum = 'Mevcut'";

            return await connection.QueryAsync<Car>(sql);
        }
    }
}
