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

        public async Task<IEnumerable<Motorbike>> GetAllMotorbike()
        {
            return await _postgreSQLDatabaseContext.Connection.QueryAsync<Motorbike>("SELECT * FROM \"Motorbike\"");
        }
    }
}
