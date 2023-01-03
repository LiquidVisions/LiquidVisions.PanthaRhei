using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class FieldDataTypeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_DataTypes_DataTypeName",
                table: "Fields");

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

            migrationBuilder.AlterColumn<string>(
                name: "DataTypeName",
                table: "Fields",
                type: "nvarchar(32)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(32)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("2d605faf-df2b-4418-94e4-bfb3780e1f97"), "EntityType", "class" },
                    { new Guid("359e3edf-f177-4742-a275-a05aae07c218"), "Keyword", "abstract" },
                    { new Guid("79d9af85-d339-4bcc-98e2-050bc98d7045"), "EntityType", "interface" },
                    { new Guid("a24f2014-9c49-4348-81e2-8198c29b5d79"), "EntityType", "enum" },
                    { new Guid("bb345131-c8ed-41d3-ae44-1f6fa36642e7"), "Keyword", "override" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_DataTypes_DataTypeName",
                table: "Fields",
                column: "DataTypeName",
                principalTable: "DataTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_DataTypes_DataTypeName",
                table: "Fields");

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("2d605faf-df2b-4418-94e4-bfb3780e1f97"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("359e3edf-f177-4742-a275-a05aae07c218"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("79d9af85-d339-4bcc-98e2-050bc98d7045"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("a24f2014-9c49-4348-81e2-8198c29b5d79"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("bb345131-c8ed-41d3-ae44-1f6fa36642e7"), "Keyword" });

            migrationBuilder.AlterColumn<string>(
                name: "DataTypeName",
                table: "Fields",
                type: "nvarchar(32)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(32)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_DataTypes_DataTypeName",
                table: "Fields",
                column: "DataTypeName",
                principalTable: "DataTypes",
                principalColumn: "Name");
        }
    }
}
