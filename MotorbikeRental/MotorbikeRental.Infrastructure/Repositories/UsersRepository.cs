using Dapper;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using Npgsql;

namespace MotorbikeRental.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly PostgreSQLDatabaseContext _postgreSQLDatabaseContext;
        private NpgsqlTransaction _transaction;

        public UsersRepository(PostgreSQLDatabaseContext postgreSQLDatabaseContext)
        {
            _postgreSQLDatabaseContext = postgreSQLDatabaseContext;
        }

        public async Task<Users> GetUser(string name, string password)
        {
            BeginTransaction();

            try
            {
                var result = await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<Users>(
                    @"SELECT 
                        * 
                      FROM 
	                      users
		                      INNER JOIN usertype ON usertype.id = users.id
                      WHERE 
	                      users.name = @name
		                      AND users.password = @password"
                    , new { name, password }
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

        public async Task UpdateToken(int id, string token, DateTime tokenDateExpire)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                    @"UPDATE 
	                    users
                    SET 
	                    token=@token
	                    , tokendateexpire=@tokenDateExpire
                    WHERE 
	                    id = @id"
                , new { id, token, tokenDateExpire }
                , _transaction);
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
        }

        private void BeginTransaction()
        {
            _transaction = _postgreSQLDatabaseContext.Connection.BeginTransaction();
        }
    }
}