using FluentMigrator;

namespace Restaurant.Migrations
{
    [Migration(202210041800)]
    public sealed class InitCreateTable_04_10_2022_18_00 : Migration
    {
        public override void Down()
        {
            Delete.Table("products");
        }

        public override void Up()
        {
            Create.Table("products")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("ProductName").AsString(150).NotNullable().Indexed("idx_products_product_name")
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("ProductKind").AsInt32().NotNullable();
        }
    }
}
