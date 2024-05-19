using Dapper;
using Drifters.Application.Interfaces;
using Drifters.Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Drifters.Infrastructure.Data;

public class DriverRepository : IDriverRepository
{
    private readonly string _connectionString;

    public DriverRepository(string connectionString)
    {
        _connectionString = connectionString;
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
        await connection.ExecuteAsync(sql, driver);
    }

    public async Task UpdateDriverAsync(Driver driver)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "UPDATE Driver SET Name = @Name, Nationality = @Nationality, ChampionshipsWon = @ChampionshipsWon WHERE Id = @Id";
        await connection.ExecuteAsync(sql, driver);
    }

    public async Task DeleteDriverAsync(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "DELETE FROM Driver WHERE Id = @Id";
        await connection.ExecuteAsync(sql, new { Id = id });
    }
}
