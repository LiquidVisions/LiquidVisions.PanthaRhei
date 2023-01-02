using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedEntitiesAndFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("044f563e-a622-40e7-954e-cb2777a6fa86"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("24bac302-3aca-466d-b0a5-d0e830dff8f1"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("79749511-18d3-4f14-b573-1c0fc20238a4"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("82a06781-d12f-42df-aa71-067d73cba845"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("a16a56cf-caf2-4e69-80e2-ddfd212b8289"), "EntityType" });

            migrationBuilder.DropColumn(
                name: "IsCollection",
                table: "Fields");

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("6d836ad2-6478-4fe7-ac2f-fe3726b68759"), "Keyword", "abstract" },
                    { new Guid("8c9ebfa5-0f87-4d24-86f8-77abaa30a17d"), "EntityType", "class" },
                    { new Guid("8ff18d06-1d6d-425a-8180-1403399f74c1"), "EntityType", "interface" },
                    { new Guid("aea9e3e7-39ce-4468-8e0d-c0137425316c"), "Keyword", "override" },
                    { new Guid("cebc1a0d-be52-4be2-a818-765a6791c8d8"), "EntityType", "enum" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("6d836ad2-6478-4fe7-ac2f-fe3726b68759"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("8c9ebfa5-0f87-4d24-86f8-77abaa30a17d"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("8ff18d06-1d6d-425a-8180-1403399f74c1"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("aea9e3e7-39ce-4468-8e0d-c0137425316c"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("cebc1a0d-be52-4be2-a818-765a6791c8d8"), "EntityType" });

            migrationBuilder.AddColumn<bool>(
                name: "IsCollection",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("044f563e-a622-40e7-954e-cb2777a6fa86"), "Keyword", "abstract" },
                    { new Guid("24bac302-3aca-466d-b0a5-d0e830dff8f1"), "EntityType", "enum" },
                    { new Guid("79749511-18d3-4f14-b573-1c0fc20238a4"), "Keyword", "override" },
                    { new Guid("82a06781-d12f-42df-aa71-067d73cba845"), "EntityType", "interface" },
                    { new Guid("a16a56cf-caf2-4e69-80e2-ddfd212b8289"), "EntityType", "class" }
                });
        }
    }
}
