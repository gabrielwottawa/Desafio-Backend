﻿using Npgsql;

namespace MotorbikeRental.Infrastructure.PostgreSQL
{
    public abstract class PostgreSQLDatabaseContext : IDisposable
    {
        public NpgsqlConnection Connection { get; }

        protected PostgreSQLDatabaseContext(string connectionString)
        {
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