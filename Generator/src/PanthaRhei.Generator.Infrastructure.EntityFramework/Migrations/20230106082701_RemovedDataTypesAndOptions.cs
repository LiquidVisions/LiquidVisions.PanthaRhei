using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemovedDataTypesAndOptions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_DataTypes_DataTypeName",
                table: "Fields");

            migrationBuilder.DropTable(
                name: "DataTypes");

            migrationBuilder.DropTable(
                name: "EntityOptions");

            migrationBuilder.DropTable(
                name: "FieldOptions");

            migrationBuilder.DropTable(
                name: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Fields_DataTypeName",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "DataTypeName",
                table: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "ReturnType",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnType",
                table: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "DataTypeName",
                table: "Fields",
                type: "nvarchar(32)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Fields_DataTypeName",
                table: "Fields",
                column: "DataTypeName");

            migrationBuilder.CreateIndex(
                name: "IX_EntityOptions_OptionsId_OptionsKey",
                table: "EntityOptions",
                columns: new[] { "OptionsId", "OptionsKey" });

            migrationBuilder.CreateIndex(
                name: "IX_FieldOptions_OptionsId_OptionsKey",
                table: "FieldOptions",
                columns: new[] { "OptionsId", "OptionsKey" });

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_DataTypes_DataTypeName",
                table: "Fields",
                column: "DataTypeName",
                principalTable: "DataTypes",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
