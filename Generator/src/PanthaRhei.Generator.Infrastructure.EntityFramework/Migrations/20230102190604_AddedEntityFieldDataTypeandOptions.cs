using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedEntityFieldDataTypeandOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataTypes",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataTypes", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Options",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Options", x => new { x.Id, x.Key });
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    IsCollection = table.Column<bool>(type: "bit", nullable: false),
                    DataTypeName = table.Column<string>(type: "nvarchar(32)", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_DataTypes_DataTypeName",
                        column: x => x.DataTypeName,
                        principalTable: "DataTypes",
                        principalColumn: "Name");
                    table.ForeignKey(
                        name: "FK_Fields_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityOptions",
                columns: table => new
                {
                    EntitiesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionsKey = table.Column<string>(type: "nvarchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntityOptions", x => new { x.EntitiesId, x.OptionsId, x.OptionsKey });
                    table.ForeignKey(
                        name: "FK_EntityOptions_Entities_EntitiesId",
                        column: x => x.EntitiesId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EntityOptions_Options_OptionsId_OptionsKey",
                        columns: x => new { x.OptionsId, x.OptionsKey },
                        principalTable: "Options",
                        principalColumns: new[] { "Id", "Key" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FieldOptions",
                columns: table => new
                {
                    FieldsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OptionsKey = table.Column<string>(type: "nvarchar(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldOptions", x => new { x.FieldsId, x.OptionsId, x.OptionsKey });
                    table.ForeignKey(
                        name: "FK_FieldOptions_Fields_FieldsId",
                        column: x => x.FieldsId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldOptions_Options_OptionsId_OptionsKey",
                        columns: x => new { x.OptionsId, x.OptionsKey },
                        principalTable: "Options",
                        principalColumns: new[] { "Id", "Key" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                    { new Guid("044f563e-a622-40e7-954e-cb2777a6fa86"), "Keyword", "abstract" },
                    { new Guid("24bac302-3aca-466d-b0a5-d0e830dff8f1"), "EntityType", "enum" },
                    { new Guid("79749511-18d3-4f14-b573-1c0fc20238a4"), "Keyword", "override" },
                    { new Guid("82a06781-d12f-42df-aa71-067d73cba845"), "EntityType", "interface" },
                    { new Guid("a16a56cf-caf2-4e69-80e2-ddfd212b8289"), "EntityType", "class" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EntityOptions_OptionsId_OptionsKey",
                table: "EntityOptions",
                columns: new[] { "OptionsId", "OptionsKey" });

            migrationBuilder.CreateIndex(
                name: "IX_FieldOptions_OptionsId_OptionsKey",
                table: "FieldOptions",
                columns: new[] { "OptionsId", "OptionsKey" });

            migrationBuilder.CreateIndex(
                name: "IX_Fields_DataTypeName",
                table: "Fields",
                column: "DataTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_EntityId",
                table: "Fields",
                column: "EntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EntityOptions");

            migrationBuilder.DropTable(
                name: "FieldOptions");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropTable(
                name: "DataTypes");

            migrationBuilder.DropTable(
                name: "Entities");
        }
    }
}
