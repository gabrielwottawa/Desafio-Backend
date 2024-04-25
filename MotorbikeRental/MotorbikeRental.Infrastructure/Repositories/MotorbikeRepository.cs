using Dapper;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Infrastructure.Repositories
{
    public class MotorbikeRepository : IMotorbikeRepository
    {
        private readonly PostgreSQLDatabaseContext _postgreSQLDatabaseContext;

        public MotorbikeRepository(PostgreSQLDatabaseContext postgreSQLDatabaseContext)
        {
            _postgreSQLDatabaseContext = postgreSQLDatabaseContext;
        }

        public async Task<IEnumerable<Motorbikes>> GetAllMotorbikes(string? plate)
        {
            var sqlCommand = "SELECT * FROM motorbike";

            if (plate != null)
                sqlCommand += " WHERE plate = @plate";

            return await _postgreSQLDatabaseContext.Connection.QueryAsync<Motorbikes>(sqlCommand, new { plate });
        }

        public async Task<Motorbikes> GetMotorbikeByPlate(string plate)
        {
            return await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<Motorbikes>(
                        @"SELECT 
                            * 
                        FROM 
                            motorbike
                        WHERE
                            plate = @plate", new { plate });
        }

        public async Task<Motorbikes> GetMotorbikeById(int id)
        {
            return await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<Motorbikes>(
                        @"SELECT 
                            * 
                        FROM 
                            motorbike
                        WHERE
                            id = @id", new { id });
        }

        public async Task InsertMotorbike(Motorbikes motorbike)
        {
            await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                        @"INSERT INTO motorbike(plate, year, type)
	                            VALUES (@plate, @year, @type)"
                        , new { motorbike.Plate, motorbike.Year, motorbike.Type });
        }

        public async Task UpdateMotorbike(int id, string plate)
        {
            await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                    @"UPDATE motorbike SET plate = @plate WHERE id = @id"
                    , new { id, plate });
        }
    }
}
