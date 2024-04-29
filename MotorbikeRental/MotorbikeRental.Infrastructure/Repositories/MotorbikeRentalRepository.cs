using Dapper;
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

        public async Task InsertMotorbikeRentalAsync(MotorbikeRentals motorbikeRentals)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                        @"INSERT INTO motorbikerentals(startdate, enddate, estimatedenddate, rentalplansid, motorbikeid, motorbikeplate, courierid, couriercnpj, courierregisternumber, activerental, status)
	                        VALUES (@startdate, @enddate, @estimatedenddate, @rentalplansid, @motorbikeid, @motorbikeplate, @courierid, @couriercnpj, @courierregisternumber, @activerental, @status)"
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
                            motorbikeRentals.CourierRegisterNumber,
                            motorbikeRentals.ActiveRental,
                            motorbikeRentals.Status
                        }
                        , _transaction);

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
            }
        }

        public async Task<bool> IsRentedMotorbikeAsync(string plate)
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

        public bool IsRentedMotorbikes(string plate)
        {
            var result = _postgreSQLDatabaseContext.Connection.QuerySingleOrDefault<MotorbikeRentals>(
                        @"SELECT 
                            * 
                        FROM 
                            motorbikerentals
                        WHERE
                            motorbikeplate = @plate
                                AND activerental = 1"
                        , new { plate }
                        );

            return result != null;
        }

        public void InsertMotorbikeRentals(MotorbikeRentals motorbikeRentals)
        {
            _postgreSQLDatabaseContext.Connection.Execute(
                    @"INSERT INTO motorbikerentals(startdate, enddate, estimatedenddate, rentalplansid, motorbikeid, motorbikeplate, courierid, couriercnpj, courierregisternumber, activerental, status)
	                        VALUES (@startdate, @enddate, @estimatedenddate, @rentalplansid, @motorbikeid, @motorbikeplate, @courierid, @couriercnpj, @courierregisternumber, @activerental, @status)"
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
                        motorbikeRentals.CourierRegisterNumber,
                        motorbikeRentals.ActiveRental,
                        motorbikeRentals.Status
                    });
        }

        private void BeginTransaction()
        {
            _transaction = _postgreSQLDatabaseContext.Connection.BeginTransaction();
        }

        public async Task<MotorbikeRentals> GetMotorbikeRentalsAsync(string motorbikePlate, string courierCnpj, string courierRegisterNumber)
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
                            motorbikeplate = @motorbikePlate
                                AND activerental = 1
                                    AND couriercnpj = @courierCnpj
                                        AND courierregisternumber = @courierRegisterNumber"
                        , new { motorbikePlate, courierCnpj, courierRegisterNumber }
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
    }
}