using Dapper;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;

namespace MotorbikeRental.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly PostgreSQLDatabaseContext _postgreSQLDatabaseContext;

        public UsersRepository(PostgreSQLDatabaseContext postgreSQLDatabaseContext)
        {
            _postgreSQLDatabaseContext = postgreSQLDatabaseContext;
        }

        public async Task<Users> GetUser(string name, string password)
        {
            return await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<Users>(
                    @"SELECT 
                        * 
                      FROM 
	                      users
		                      INNER JOIN usertype ON usertype.id = users.id
                      WHERE 
	                      users.name = @name
		                      AND users.password = @password"
                    , new { name, password });
        }

        public async Task UpdateToken(int id, string token, DateTime tokenDateExpire)
        {
            await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                @"UPDATE 
	                users
                SET 
	                token=@token
	                , tokendateexpire=@tokenDateExpire
                WHERE 
	                id = @id", new { id, token, tokenDateExpire });
        }
    }
}