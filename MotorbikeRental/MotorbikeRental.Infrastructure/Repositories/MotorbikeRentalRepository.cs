﻿using Dapper;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using Npgsql;

namespace MotorbikeRental.Infrastructure.Repositories
{
    public class MotorbikeRentalRepository : IMotorbikeRentalRepository
    {
        private readonly PostgreSQLDatabaseContext _postgreSQLDatabaseContext;
        private NpgsqlTransaction _transaction;

        public MotorbikeRentalRepository(PostgreSQLDatabaseContext postgreSQLDatabaseContext)
        {
            _postgreSQLDatabaseContext = postgreSQLDatabaseContext;
        }

        public async Task InsertMotorbikeRental(MotorbikeRentals motorbikeRentals)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                        @"INSERT INTO motorbikerentals(startdate, enddate, estimatedenddate, rentalplansid, motorbikeid, motorbikeplate, courierid, couriercnpj, courierregisternumber)
	                        VALUES (@startdate, @enddate, @estimatedenddate, @rentalplansid, @motorbikeid, @motorbikeplate, @courierid, @couriercnpj, @courierregisternumber)"
                        , new
                        {
                            motorbikeRentals.StartDate,
                            motorbikeRentals.EndDate,
                            motorbikeRentals.EstimatedEndDate,
                            motorbikeRentals.RentalPlansId,
                            motorbikeRentals.MotorbikeId,
                            motorbikeRentals.MotorbikePlate,
                            motorbikeRentals.CourierId,
                            motorbikeRentals.CourierCnpj,
                            motorbikeRentals.CourierRegisterNumber
                        }
                        , _transaction);

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
            }
        }

        public async Task<bool> MotorbikeIsRented(string plate)
        {
            BeginTransaction();

            try
            {
                var result = await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<MotorbikeRentals>(
                        @"SELECT 
                            * 
                        FROM 
                            motorbikerentals
                        WHERE
                            motorbikeplate = @plate
                                AND activerental = 1"
                        , new { plate }
                        , _transaction
                        );

                _transaction.Commit();

                return result != null;
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