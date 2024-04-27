using FluentMigrator;

namespace MotorbikeRental.Infrastructure.Migrations.Migrations
{
    [Migration(1)]
    public class Migrations_v1 : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Create.Table("usertype")
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("name").AsString().NotNullable();

            Create.Table("users")
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("name").AsString().NotNullable()
                .WithColumn("password").AsString().NotNullable()
                .WithColumn("usertypeid").AsInt32().NotNullable().ForeignKey("FK_UserTypeId", "usertype", "id")
                .WithColumn("token").AsString().Nullable()
                .WithColumn("tokendateexpire").AsDateTime().Nullable();

            Create.Table("motorbike")
                .WithColumn("id").AsInt32().Identity().NotNullable()
                .WithColumn("plate").AsString(10).NotNullable()
                .WithColumn("year").AsInt32().NotNullable()
                .WithColumn("type").AsString(50).NotNullable();

            Create.PrimaryKey("PK_Motorbike").OnTable("motorbike").Columns("id", "plate");

            Create.Table("registertype")
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("type").AsString().NotNullable();

            Create.Table("couriers")
                .WithColumn("id").AsInt32().Identity().NotNullable()
                .WithColumn("name").AsString(100).NotNullable()
                .WithColumn("cnpj").AsString(25).NotNullable()
                .WithColumn("dateofbirth").AsDate().NotNullable()
                .WithColumn("registernumber").AsString().NotNullable()
                .WithColumn("registertypeid").AsInt32().NotNullable().ForeignKey("FK_RegisterTypeId", "registertype", "id")
                .WithColumn("urlimage").AsString(100).Nullable();

            Create.PrimaryKey("PK_Couriers").OnTable("couriers").Columns("id", "cnpj", "registernumber");

            Create.Table("rentalplans")
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("numberdays").AsInt32().NotNullable()
                .WithColumn("valueperday").AsDecimal().NotNullable();

            Create.Table("motorbikerentals")
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("startdate").AsDateTime().NotNullable()
                .WithColumn("enddate").AsDateTime().NotNullable()
                .WithColumn("estimatedenddate").AsDateTime().NotNullable()
                .WithColumn("rentalplansid").AsInt32().NotNullable().ForeignKey("FK_RentalPlansId", "rentalplans", "id")
                .WithColumn("motorbikeid").AsInt32().NotNullable().ForeignKey()
                .WithColumn("motorbikeplate").AsString(10).NotNullable().ForeignKey()
                .WithColumn("courierid").AsInt32().NotNullable()
                .WithColumn("couriercnpj").AsString(25).NotNullable()
                .WithColumn("courierregisternumber").AsString().NotNullable()
                .WithColumn("activerental").AsInt32().WithDefaultValue(1).NotNullable();

            Create.ForeignKey("FK_Motorbike")
                .FromTable("motorbikerentals").ForeignColumns("motorbikeid", "motorbikeplate")
                .ToTable("motorbike").PrimaryColumns("id", "plate");

            Create.ForeignKey("FK_Courier")
                .FromTable("motorbikerentals").ForeignColumns("courierid", "couriercnpj", "courierregisternumber")
                .ToTable("couriers").PrimaryColumns("id", "cnpj", "registernumber");

            InsertDataRegisterType();
            InsertDataUserType();
            InsertDataUsers();
            InsertDataRentalPlans();
        }

        private void InsertDataRegisterType()
        {
            InsertRegisterType("A");
            InsertRegisterType("B");
            InsertRegisterType("AB");
        }

        private void InsertRegisterType(string type)
        {
            Insert.IntoTable("registertype").Row(new { type });
        }

        private void InsertDataUserType()
        {
            InsertUserType("admin");
            InsertUserType("user");
        }

        private void InsertUserType(string name)
        {
            Insert.IntoTable("usertype").Row(new { name });
        }

        private void InsertDataUsers()
        {
            InsertUsers("admin", "123", 1);
            InsertUsers("entregador", "123", 2);
        }

        private void InsertUsers(string name, string password, int usertypeid)
        {
            Insert.IntoTable("users").Row(new { name, password, usertypeid });
        }

        private void InsertDataRentalPlans()
        {
            InsertRentalPlans(7, 30.00);
            InsertRentalPlans(15, 28.00);
            InsertRentalPlans(30, 22.00);
            InsertRentalPlans(45, 20.00);
            InsertRentalPlans(50, 18.00);
        }

        public void InsertRentalPlans(int numberdays, double valueperday)
        {
            Insert.IntoTable("rentalplans").Row(new { numberdays, valueperday });
        }
    }
}