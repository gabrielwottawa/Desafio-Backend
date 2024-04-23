using FluentMigrator;

namespace MotorbikeRental.Infrastructure.Migrations.Migrations
{
    [Migration(2)]
    public class Migrations_v1 : Migration
    {
        public override void Down()
        {
            //Delete.Table("Motorbike");
            //Delete.ForeignKey("FK_RegisterType_Deliveryman");
            //Delete.Table("Deliveryman");
            //Delete.Table("RegisterType");
        }

        public override void Up()
        {
            Create.Table("Motorbike")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("Plate").AsString(10).NotNullable().PrimaryKey()
                .WithColumn("Year").AsInt64().NotNullable()
                .WithColumn("Type").AsString(50).NotNullable();

            Create.Table("Deliveryman")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("CNPJ").AsString(25).NotNullable().PrimaryKey()
                .WithColumn("DateOfBirth").AsDate().NotNullable()
                .WithColumn("RegisterNumber").AsString().NotNullable().PrimaryKey()
                .WithColumn("RegisterTypeId").AsInt32().NotNullable()
                .WithColumn("UrlImage").AsString(100).NotNullable();

            Create.Table("RegisterType")
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey()
                .WithColumn("Type").AsString().NotNullable();

            Create.ForeignKey("FK_RegisterType_Deliveryman")
                .FromTable("Deliveryman").ForeignColumn("RegisterTypeId")
                .ToTable("RegisterType").PrimaryColumn("Id");

            InsertDataRegisterType();
        }

        private void InsertDataRegisterType()
        {
            InsertRegisterType("A");
            InsertRegisterType("B");
            InsertRegisterType("AB");
        }

        private void InsertRegisterType(string type)
        {
            Insert.IntoTable("RegisterType").Row(new { Type = type });
        }
    }
}