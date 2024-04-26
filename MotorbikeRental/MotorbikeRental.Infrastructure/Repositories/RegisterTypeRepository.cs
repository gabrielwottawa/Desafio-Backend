﻿using Dapper;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using Npgsql;

namespace MotorbikeRental.Infrastructure.Repositories
{
    public class RegisterTypeRepository : IRegisterTypeRepository
    {
        private readonly PostgreSQLDatabaseContext _postgreSQLDatabaseContext;
        private NpgsqlTransaction _transaction;

        public RegisterTypeRepository(PostgreSQLDatabaseContext postgreSQLDatabaseContext)
        {
            _postgreSQLDatabaseContext = postgreSQLDatabaseContext;
        }

        public Task<RegisterType> GetRegisterTypeByType(string registerType)
        {
            BeginTransaction();

            try
            {
                var result = _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<RegisterType>(
                                @"SELECT * FROM registertype WHERE type = @registerType"
                                , new { registerType }
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

        private void BeginTransaction()
        {
            _transaction = _postgreSQLDatabaseContext.Connection.BeginTransaction();
        }
    }
}