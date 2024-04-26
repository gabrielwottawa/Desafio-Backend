using Dapper;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using Npgsql;

namespace MotorbikeRental.Infrastructure.Repositories
{
    public class MotorbikeRepository : IMotorbikeRepository
    {
        private readonly PostgreSQLDatabaseContext _postgreSQLDatabaseContext;
        private NpgsqlTransaction _transaction;

        public MotorbikeRepository(PostgreSQLDatabaseContext postgreSQLDatabaseContext)
        {
            _postgreSQLDatabaseContext = postgreSQLDatabaseContext;
        }

        public async Task<IEnumerable<Motorbikes>> GetAllMotorbikes(string? plate)
        {
            BeginTransaction();

            try
            {
                var sqlCommand = "SELECT * FROM motorbike";

                if (plate != null)
                    sqlCommand += " WHERE plate = @plate";

                var result = await _postgreSQLDatabaseContext.Connection.QueryAsync<Motorbikes>(
                                sqlCommand
                                , new { plate }
                                , _transaction);

                _transaction.Commit();

                return result;
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
        }

        public async Task<Motorbikes> GetMotorbikeByPlate(string plate)
        {
            BeginTransaction();

            try
            {
                var result = await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<Motorbikes>(
                        @"SELECT 
                            * 
                        FROM 
                            motorbike
                        WHERE
                            plate = @plate"
                        , new { plate }
                        , _transaction
                        );

                _transaction.Commit();

                return result;
            }
            catch (Exception) 
            {
                _transaction.Rollback();
                throw;
            }
        }

        public async Task<Motorbikes> GetMotorbikeById(int id)
        {
            BeginTransaction();

            try
            {
                var result =  await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<Motorbikes>(
                        @"SELECT 
                            * 
                        FROM 
                            motorbike
                        WHERE
                            id = @id"
                        , new { id }
                        , _transaction
                        );

                _transaction.Commit();

                return result;
            } 
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
        }

        public async Task InsertMotorbike(Motorbikes motorbike)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                        @"INSERT INTO motorbike(plate, year, type)
	                            VALUES (@plate, @year, @type)"
                        , new { motorbike.Plate, motorbike.Year, motorbike.Type }
                        , _transaction);

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
            }
        }

        public async Task UpdateMotorbike(int id, string plate)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                    @"UPDATE motorbike SET plate = @plate WHERE id = @id"
                    , new { id, plate }
                    , _transaction);

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
            }
        }

        public async Task DeleteMotorbikeById(int id)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                            @"DELETE FROM motorbike WHERE id = @id"
                            , new { id }
                            , _transaction);

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
            }
        }

        private void BeginTransaction()
        {
            _transaction = _postgreSQLDatabaseContext.Connection.BeginTransaction();
        }
    }
}