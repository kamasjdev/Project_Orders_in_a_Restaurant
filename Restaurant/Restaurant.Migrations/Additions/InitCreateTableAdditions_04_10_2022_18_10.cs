using FluentMigrator;

namespace Restaurant.Migrations.Additions
{
    [Migration(202210041810)]
    public sealed class InitCreateTableAdditions_04_10_2022_18_10 : Migration
    {
        public override void Down()
        {
            Delete.Table("additions");
        }

        public override void Up()
        {
            Create.Table("additions")
                .WithColumn("Id").AsGuid().PrimaryKey()
                .WithColumn("AdditionName").AsString(150).NotNullable().Indexed("idx_additions_addition_name")
                .WithColumn("Price").AsDecimal().NotNullable()
                .WithColumn("ProductKind").AsInt32();
        }
    }
}
