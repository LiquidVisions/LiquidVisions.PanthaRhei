using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddComponentsAppRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AppId",
                table: "Components",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Components_AppId",
                table: "Components",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Apps_AppId",
                table: "Components",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Components_Apps_AppId",
                table: "Components");

            migrationBuilder.DropIndex(
                name: "IX_Components_AppId",
                table: "Components");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "Components");
        }
    }
}
