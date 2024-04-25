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
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("plate").AsString(10).NotNullable().PrimaryKey()
                .WithColumn("year").AsInt32().NotNullable()
                .WithColumn("type").AsString(50).NotNullable();

            Create.Table("registertype")
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("type").AsString().NotNullable();

            Create.Table("deliveryman")
                .WithColumn("id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("name").AsString(100).NotNullable()
                .WithColumn("cnpj").AsString(25).NotNullable().PrimaryKey()
                .WithColumn("dateofbirth").AsDate().NotNullable()
                .WithColumn("registernumber").AsString().NotNullable().PrimaryKey()
                .WithColumn("registertypeid").AsInt32().NotNullable().ForeignKey("FK_RegisterTypeId", "registertype", "id")
                .WithColumn("urlimage").AsString(100).NotNullable();

            InsertDataRegisterType();
            InsertDataUserType();
            InsertDataUsers();
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
    }
}