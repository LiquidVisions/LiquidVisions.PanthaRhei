using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class EnhancedFieldObjectWithAditionalProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Behaviour",
                table: "Fields",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GetModifier",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCollection",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Fields",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "public");

            migrationBuilder.AddColumn<Guid>(
                name: "ReferenceId",
                table: "Fields",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SetModifier",
                table: "Fields",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_ReferenceId",
                table: "Fields",
                column: "ReferenceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Entities_ReferenceId",
                table: "Fields",
                column: "ReferenceId",
                principalTable: "Entities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Entities_ReferenceId",
                table: "Fields");

            migrationBuilder.DropIndex(
                name: "IX_Fields_ReferenceId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Behaviour",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "GetModifier",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "IsCollection",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "ReferenceId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "SetModifier",
                table: "Fields");
        }
    }
}
