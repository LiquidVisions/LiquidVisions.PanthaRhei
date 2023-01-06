using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedAppEntitiesRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Apps_AppId",
                table: "Entities");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppId",
                table: "Entities",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Apps_AppId",
                table: "Entities",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entities_Apps_AppId",
                table: "Entities");

            migrationBuilder.AlterColumn<Guid>(
                name: "AppId",
                table: "Entities",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Entities_Apps_AppId",
                table: "Entities",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id");
        }
    }
}
