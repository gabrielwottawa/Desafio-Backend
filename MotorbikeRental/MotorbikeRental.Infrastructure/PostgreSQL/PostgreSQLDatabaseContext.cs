using Npgsql;
using System.Data;

namespace MotorbikeRental.Infrastructure.PostgreSQL
{
    public abstract class PostgreSQLDatabaseContext : IDisposable
    {
        public NpgsqlConnection Connection { get; }
        public IDbTransaction Transaction { get; set; }

        protected PostgreSQLDatabaseContext(string connectionString)
        {
            // Cria e abre uma conexão com o PostgreSQL
            NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            Connection = connection;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) 
        { 
            Connection?.Close();
            Connection?.Dispose();
        }
    }
}