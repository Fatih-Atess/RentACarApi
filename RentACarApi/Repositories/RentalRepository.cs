using Dapper;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using RentACarApi.DTOs;
using RentACarApi.Models;
using RentACarApi.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace RentACarApi.Repositories
{
    public class RentalRepository: IRentalRepository
    {
        private readonly string _connectionString;

        public RentalRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<bool> IsCarAvailableForDatesAsync(int aracId, DateTime startDate, DateTime endDate)
        {
            using var connection = new MySqlConnection(_connectionString);

            string sql = @"
                SELECT COUNT(*) FROM Rentals
                WHERE Arac_ID = @AracId
                AND Baslangic_Tarihi <= @EndDate
                AND Bitis_Tarihi >= @StartDate";
            var conflictCount = await connection.ExecuteScalarAsync<int>(sql, new { AracId = aracId, StartDate = startDate, EndDate = endDate });
            return conflictCount == 0;
        }

        public async Task<bool> IsCarExist(int id)
        {
            using var connection = new MySqlConnection(_connectionString);

            var sql = "SELECT CASE WHEN EXISTS (SELECT 1 FROM Cars WHERE Arac_ID = @Id) THEN 1 ELSE 0 END";

            return await connection.ExecuteScalarAsync<bool>(sql, new { Id = id });
        }

        public async Task<bool> RentCarWithTransactionAsync(CreateRentalRequest request)
        {
            using var connection = new MySqlConnection(_connectionString);
            await connection.OpenAsync();
            
            using var transaction = await connection.BeginTransactionAsync();

            try
            {
                string insertRentalSql = @"
                    INSERT INTO Rentals (Arac_ID, Musteri_Adi, Baslangic_Tarihi, Bitis_Tarihi, Toplam_Tutar) 
                    VALUES (@Arac_ID, @Musteri_Adi, @Baslangic_Tarihi, @Bitis_Tarihi, @Toplam_Tutar)";

                await connection.ExecuteAsync(insertRentalSql, request, transaction);

                string updateCarSql = "UPDATE Cars SET Durum = 'Kirada' WHERE Arac_ID = @Arac_ID";

                await connection.ExecuteAsync(updateCarSql, new { request.Arac_ID }, transaction);

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<Rental>> GetAllRentalsAsync()
        {
            using var connection = new MySqlConnection(_connectionString);

            string sql = "SELECT * FROM Rentals";

            return await connection.QueryAsync<Rental>(sql);
        }
    }
}
