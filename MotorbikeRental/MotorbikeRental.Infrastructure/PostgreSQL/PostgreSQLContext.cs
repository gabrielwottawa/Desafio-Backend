using Microsoft.Extensions.Configuration;

namespace MotorbikeRental.Infrastructure.PostgreSQL
{
    public class PostgreSQLContext : PostgreSQLDatabaseContext
    {
        public PostgreSQLContext(IConfiguration configuration) 
            : base(configuration.GetConnectionString("PostgreSQLConnectionString"))
        {
        }
    }
}