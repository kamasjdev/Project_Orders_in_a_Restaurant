using FluentMigrator;
using System;
using System.Collections.Generic;

namespace Restaurant.Migrations.Products
{
    [Migration(202210041825)]
    public sealed class MigrationDataProducts_04_10_2022_18_25 : Migration
    {
        public override void Down()
        {
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000001") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000002") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000003") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000004") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000005") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000006") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000007") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000008") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000009") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000010") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000011") });
            Delete.FromTable("products")
                .Row(new { Id = new Guid("00000000-0000-0000-0000-000000000012") });
        }

        public override void Up()
        {
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000001") },
                    { "ProductName", "Pizza Margheritta" },
                    { "Price", 20M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000002") },
                    { "ProductName", "Pizza Vegetariana" },
                    { "Price", 22M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000003") },
                    { "ProductName", "Pizza Tosca" },
                    { "Price", 25M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000004") },
                    { "ProductName", "Pizza Venecia" },
                    { "Price", 25M },
                    { "ProductKind", 0 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000005") },
                    { "ProductName", "Schabowy z frytkami/ryżem/ziemniakami" },
                    { "Price", 30M },
                    { "ProductKind", 1 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000006") },
                    { "ProductName", "Ryba z frytkami" },
                    { "Price", 28M },
                    { "ProductKind", 1 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000007") },
                    { "ProductName", "Placek po węgiersku" },
                    { "Price", 27M },
                    { "ProductKind", 1 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000008") },
                    { "ProductName", "Zupa pomidorowa" },
                    { "Price", 12M },
                    { "ProductKind", 2 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000009") },
                    { "ProductName", "Zupa rosół" },
                    { "Price", 10M },
                    { "ProductKind", 2 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000010") },
                    { "ProductName", "Kawa" },
                    { "Price", 5M },
                    { "ProductKind", 3 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000011") },
                    { "ProductName", "Herbata" },
                    { "Price", 5M },
                    { "ProductKind", 3 }
                });
            Insert.IntoTable("products")
                .Row(new Dictionary<string, object>
                {
                    { "Id", new Guid("00000000-0000-0000-0000-000000000012") },
                    { "ProductName", "Cola" },
                    { "Price", 5M },
                    { "ProductKind", 3 }
                });
        }
    }
}
