using Dapper;
using MotorbikeRental.Infrastructure.Models;
using MotorbikeRental.Infrastructure.PostgreSQL;
using MotorbikeRental.Infrastructure.Repositories.IRepositories;
using Npgsql;

namespace MotorbikeRental.Infrastructure.Repositories
{
    public class CouriersRepository : ICouriersRepository
    {
        private readonly PostgreSQLDatabaseContext _postgreSQLDatabaseContext;
        private NpgsqlTransaction _transaction;

        public CouriersRepository(PostgreSQLDatabaseContext postgreSQLDatabaseContext)
        {
            _postgreSQLDatabaseContext = postgreSQLDatabaseContext;
        }

        public async Task<Couriers> GetCourierByCnpj(string cnpj, string registerNumber)
        {
            BeginTransaction();

            try
            {
                var result = await _postgreSQLDatabaseContext.Connection.QuerySingleOrDefaultAsync<Couriers>(
                                @"SELECT 
                                    * 
                                  FROM 
                                    couriers
                                  WHERE
                                    cnpj = @cnpj
                                        AND registernumber = @registernumber"
                                , new { cnpj, registerNumber }
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

        public async Task InsertCourierAsync(Couriers courier)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                        @"INSERT INTO couriers(name, cnpj, dateofbirth, registernumber, registertypeid)
	                        VALUES (@name, @cnpj, @dateofbirth, @registernumber, @registertypeid)"
                        , new { courier.Name, courier.Cnpj, courier.DateOfBirth, courier.RegisterNumber, courier.RegisterTypeId }
                        , _transaction);

                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
        }

        public async Task InsertUrlImage(int id, string urlImage)
        {
            BeginTransaction();

            try
            {
                await _postgreSQLDatabaseContext.Connection.ExecuteAsync(
                        @"UPDATE couriers SET urlimage = @urlImage WHERE id = @id"
                , new { id, urlImage }
                , _transaction);
                _transaction.Commit();
            }
            catch (Exception)
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void InsertCourier(Couriers courier)
        {
            _postgreSQLDatabaseContext.Connection.Execute(
                   @"INSERT INTO couriers(name, cnpj, dateofbirth, registernumber, registertypeid)
	                        VALUES (@name, @cnpj, @dateofbirth, @registernumber, @registertypeid)"
                   , new { courier.Name, courier.Cnpj, courier.DateOfBirth, courier.RegisterNumber, courier.RegisterTypeId }
                   );
        }


        private void BeginTransaction()
        {
            _transaction = _postgreSQLDatabaseContext.Connection.BeginTransaction();
        }
    }
}