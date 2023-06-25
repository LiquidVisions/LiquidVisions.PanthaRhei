using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedHandlerEntityToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Handler_Expanders_ExpanderId",
                table: "Handler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Handler",
                table: "Handler");

            migrationBuilder.RenameTable(
                name: "Handler",
                newName: "Handlers");

            migrationBuilder.RenameIndex(
                name: "IX_Handler_ExpanderId",
                table: "Handlers",
                newName: "IX_Handlers_ExpanderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Handlers",
                table: "Handlers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Handlers_Expanders_ExpanderId",
                table: "Handlers",
                column: "ExpanderId",
                principalTable: "Expanders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Handlers_Expanders_ExpanderId",
                table: "Handlers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Handlers",
                table: "Handlers");

            migrationBuilder.RenameTable(
                name: "Handlers",
                newName: "Handler");

            migrationBuilder.RenameIndex(
                name: "IX_Handlers_ExpanderId",
                table: "Handler",
                newName: "IX_Handler_ExpanderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Handler",
                table: "Handler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Handler_Expanders_ExpanderId",
                table: "Handler",
                column: "ExpanderId",
                principalTable: "Expanders",
                principalColumn: "Id");
        }
    }
}
