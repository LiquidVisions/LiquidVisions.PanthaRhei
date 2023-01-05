using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemovedVersionFromComponent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Version",
                table: "Components");

            migrationBuilder.AddColumn<Guid>(
                name: "AppId",
                table: "Entities",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("20c2975d-f693-4280-ba1f-fc60727c6911"), "EntityType", "enum" },
                    { new Guid("60cbbc7a-4bf5-41be-aeda-e9bbfa2f2764"), "EntityType", "interface" },
                    { new Guid("931b2845-6d46-4ac0-8316-99c90511ad85"), "Keyword", "abstract" },
                    { new Guid("a8945343-f1a0-465f-b201-5cac4a2ce64c"), "Keyword", "override" },
                    { new Guid("bc647b63-35ee-4527-867c-2114ab41cd8d"), "EntityType", "class" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Entities_AppId",
                table: "Entities",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Apps_AppId",
                table: "Entities",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Apps_AppId",
                table: "Entities");

            migrationBuilder.DropIndex(
                name: "IX_Entities_AppId",
                table: "Entities");

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("20c2975d-f693-4280-ba1f-fc60727c6911"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("60cbbc7a-4bf5-41be-aeda-e9bbfa2f2764"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("931b2845-6d46-4ac0-8316-99c90511ad85"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("a8945343-f1a0-465f-b201-5cac4a2ce64c"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("bc647b63-35ee-4527-867c-2114ab41cd8d"), "EntityType" });

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "Entities");

            migrationBuilder.AddColumn<string>(
                name: "Version",
                table: "Components",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

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
        }
    }
}
