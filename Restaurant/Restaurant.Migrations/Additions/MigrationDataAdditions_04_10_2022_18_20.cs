using FluentMigrator;
using System;
using System.Collections.Generic;

namespace Restaurant.Migrations.Additions
{
    [Migration(202210041820)]
    public sealed class MigrationDataAdditions_04_10_2022_18_20 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("additions")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000001") });
            Delete.FromTable("additions")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000002") });
            Delete.FromTable("additions")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000003") });
            Delete.FromTable("additions")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000004") });
            Delete.FromTable("additions")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000005") });
            Delete.FromTable("additions")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000006") });
        }

        public override void Up()
        {
            Insert.IntoTable("additions")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000001") },
                    { "AdditionName", "Podwójny ser" },
                    { "Price", 2M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("additions")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000002") },
                    { "AdditionName", "Salami" },
                    { "Price", 2M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("additions")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000003") },
                    { "AdditionName", "Szynka" },
                    { "Price", 2M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("additions")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000004") },
                    { "AdditionName", "Pieczarki" },
                    { "Price", 2M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("additions")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000005") },
                    { "AdditionName", "Bar sałatkowy" },
                    { "Price", 5M },
                    { "ProductKind", 1 }
                });
            Insert.IntoTable("additions")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000006") },
                    { "AdditionName", "Zestaw sosów" },
                    { "Price", 6M },
                    { "ProductKind", 1 }
                });
        }
    }
}
