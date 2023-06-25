using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class increadedMaxLengthPackageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "bool");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "DateTime");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "decimal");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "Entity");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "Guid");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "int");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "List");

            migrationBuilder.DeleteData(
                table: "DataTypes",
                keyColumn: "Name",
                keyValue: "string");

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("1745e4d1-62e1-4d29-8762-1c75df2c2aec"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("50e03285-3663-4f0e-ad6e-19fbd30edbe2"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("58132b56-77f7-443a-8137-ce05255136d4"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("90127df5-74b7-4d6d-9605-cdcc2348eb27"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("b3971f10-f3bd-448f-ad44-91c7d7db647f"), "EntityType" });

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Packages",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldMaxLength: 32);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Packages",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.InsertData(
                table: "DataTypes",
                column: "Name",
                values: new object[]
                {
                    "bool",
                    "DateTime",
                    "decimal",
                    "Entity",
                    "Guid",
                    "int",
                    "List",
                    "string"
                });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("1745e4d1-62e1-4d29-8762-1c75df2c2aec"), "Keyword", "abstract" },
                    { new Guid("50e03285-3663-4f0e-ad6e-19fbd30edbe2"), "EntityType", "class" },
                    { new Guid("58132b56-77f7-443a-8137-ce05255136d4"), "EntityType", "enum" },
                    { new Guid("90127df5-74b7-4d6d-9605-cdcc2348eb27"), "Keyword", "override" },
                    { new Guid("b3971f10-f3bd-448f-ad44-91c7d7db647f"), "EntityType", "interface" }
                });
        }
    }
}
