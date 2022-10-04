using FluentMigrator;

namespace Restaurant.Migrations.ProductSales
{
    [Migration(202210041815)]
    public sealed class InitCreateTableProductSales_04_10_2022_18_15 : Migration
    {
        public override void Down()
        {
            Delete.Table("product_sales");
        }

        public override void Up()
        {
            Create.Table("product_sales")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("OrderId").AsGuid().ForeignKey("Orders", "Id").Nullable().Indexed("idx_product_sales_order_id")
                .WithColumn("ProductId").AsGuid().ForeignKey("Products", "Id").Indexed("idx_product_sales_product_id")
                .WithColumn("AdditionId").AsGuid().ForeignKey("Additions", "Id").Nullable().Indexed("idx_product_sales_addition_id")
                .WithColumn("Email").AsString(300).Indexed("idx_product_sales_email")
                .WithColumn("EndPrice").AsDecimal()
                .WithColumn("ProductSaleState").AsInt32();
        }
    }
}
