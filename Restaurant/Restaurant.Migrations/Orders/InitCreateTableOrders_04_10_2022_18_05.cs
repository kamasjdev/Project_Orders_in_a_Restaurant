using FluentMigrator;
using System;

namespace Restaurant.Migrations.Orders
{
    [Migration(202210041805)]
    public sealed class InitCreateTableOrders_04_10_2022_18_05 : Migration
    {
        public override void Down()
        {
            Delete.Table("orders");
        }

        public override void Up()
        {
            Create.Table("orders")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("OrderNumber").AsString(300).NotNullable().Unique("uidx_orders_order_number")
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("Created").AsDateTime().NotNullable().Indexed("idx_orders_created")
                .WithColumn("Email").AsString(300).NotNullable().Indexed("idx_orders_email")
                .WithColumn("Note").AsString(5000).Nullable();
        }
    }
}
