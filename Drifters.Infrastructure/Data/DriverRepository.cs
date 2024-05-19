using Dapper;
using Drifters.Application.Interfaces;
using Drifters.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Drifters.Infrastructure.Data
{
    public class DriverRepository : IDriverRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<DriverRepository> _logger;

        public DriverRepository(string connectionString, ILogger<DriverRepository> logger)
        {
            _connectionString = connectionString;
            _logger = logger;
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Driver>("SELECT * FROM Driver");
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Driver>("SELECT * FROM Driver WHERE Id = @Id", new { Id = id });
        }

        public async Task AddDriverAsync(Driver driver)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "INSERT INTO Driver (Name, Nationality, ChampionshipsWon) VALUES (@Name, @Nationality, @ChampionshipsWon)";
            try
            {
                await connection.ExecuteAsync(sql, new { driver.Name, driver.Nationality, driver.ChampionshipsWon });
                _logger.LogInformation("Driver added successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding driver.");
                throw;
            }
        }

        public async Task UpdateDriverAsync(Driver driver)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "UPDATE Driver SET Name = @Name, Nationality = @Nationality, ChampionshipsWon = @ChampionshipsWon WHERE Id = @Id";
            try
            {
                await connection.ExecuteAsync(sql, new { driver.Name, driver.Nationality, driver.ChampionshipsWon, driver.Id });
                _logger.LogInformation("Driver updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating driver.");
                throw;
            }
        }

        public async Task DeleteDriverAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Driver WHERE Id = @Id";
            try
            {
                await connection.ExecuteAsync(sql, new { Id = id });
                _logger.LogInformation("Driver deleted successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting driver.");
                throw;
            }
        }
    }
}
