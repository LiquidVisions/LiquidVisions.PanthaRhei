using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addedAppExpanderManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expander_Apps_AppId",
                table: "Expander");

            migrationBuilder.DropIndex(
                name: "IX_Expander_AppId",
                table: "Expander");

            migrationBuilder.DropColumn(
                name: "AppId",
                table: "Expander");

            migrationBuilder.CreateTable(
                name: "AppExpander",
                columns: table => new
                {
                    AppsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpandersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExpander", x => new { x.AppsId, x.ExpandersId });
                    table.ForeignKey(
                        name: "FK_AppExpander_Apps_AppsId",
                        column: x => x.AppsId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppExpander_Expander_ExpandersId",
                        column: x => x.ExpandersId,
                        principalTable: "Expander",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppExpander_ExpandersId",
                table: "AppExpander",
                column: "ExpandersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppExpander");

            migrationBuilder.AddColumn<Guid>(
                name: "AppId",
                table: "Expander",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expander_AppId",
                table: "Expander",
                column: "AppId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expander_Apps_AppId",
                table: "Expander",
                column: "AppId",
                principalTable: "Apps",
                principalColumn: "Id");
        }
    }
}
