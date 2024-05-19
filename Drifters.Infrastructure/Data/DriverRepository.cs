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

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<Driver>> GetAllDriversAsync()
        {
            const string sql = "SELECT * FROM Driver";
            using var connection = CreateConnection();
            try
            {
                return await connection.QueryAsync<Driver>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all drivers.");
                throw;
            }
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            const string sql = "SELECT * FROM Driver " +
                               "WHERE Id = @Id";
            using var connection = CreateConnection();
            try
            {
                return await connection.QueryFirstOrDefaultAsync<Driver>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving driver by ID {Id}.", id);
                throw;
            }
        }

        public async Task AddDriverAsync(Driver driver)
        {
            const string sql = "INSERT INTO Driver (Name, Nationality, ChampionshipsWon) " +
                               "VALUES (@Name, @Nationality, @ChampionshipsWon)";
            using var connection = CreateConnection();
            try
            {
                await connection.ExecuteAsync(sql, new { driver.Name, driver.Nationality, driver.ChampionshipsWon });
                _logger.LogInformation("Driver {Name} added successfully.", driver.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding driver {Name}.", driver.Name);
                throw;
            }
        }

        public async Task UpdateDriverAsync(Driver driver)
        {
            const string sql = "UPDATE Driver " +
                               "SET Name = @Name, Nationality = @Nationality, ChampionshipsWon = @ChampionshipsWon " +
                               "WHERE Id = @Id";
            using var connection = CreateConnection();
            try
            {
                await connection.ExecuteAsync(sql, new
                {
                    driver.Name, driver.Nationality, driver.ChampionshipsWon, driver.Id
                });
                _logger.LogInformation("Driver {Id} updated successfully.", driver.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating driver {Id}.", driver.Id);
                throw;
            }
        }

        public async Task DeleteDriverAsync(int id)
        {
            const string sql = "DELETE FROM Driver " +
                               "WHERE Id = @Id";
            using var connection = CreateConnection();
            try
            {
                await connection.ExecuteAsync(sql, new { Id = id });
                _logger.LogInformation("Driver {Id} deleted successfully.", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting driver {Id}.", id);
                throw;
            }
        }
    }
}
