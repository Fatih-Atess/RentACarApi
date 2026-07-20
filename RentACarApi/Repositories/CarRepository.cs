using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RentACarApi.Models;
using RentACarApi.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentACarApi.DTOs;

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

        public async Task<bool> AddCarAsync(AddCarDTO car)
        {
            using var connection = new MySqlConnection(_connectionString);

            await connection.OpenAsync();

            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                string insertCarSql = @"
                    INSERT INTO Cars (Marka_Model, Plaka, Gunluk_Fiyat) 
                    VALUES (@Marka_Model, @Plaka, @Gunluk_fiyat)";

                await connection.ExecuteAsync(insertCarSql, car, transaction);

                await transaction.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
