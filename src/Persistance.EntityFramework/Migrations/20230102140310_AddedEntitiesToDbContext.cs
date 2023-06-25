using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedEntitiesToDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppExpander_Expander_ExpandersId",
                table: "AppExpander");

            migrationBuilder.DropForeignKey(
                name: "FK_Component_Expander_ExpanderId",
                table: "Component");

            migrationBuilder.DropForeignKey(
                name: "FK_Handler_Expander_ExpanderId",
                table: "Handler");

            migrationBuilder.DropForeignKey(
                name: "FK_Package_Component_ComponentId",
                table: "Package");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Package",
                table: "Package");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expander",
                table: "Expander");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Component",
                table: "Component");

            migrationBuilder.RenameTable(
                name: "Package",
                newName: "Packages");

            migrationBuilder.RenameTable(
                name: "Expander",
                newName: "Expanders");

            migrationBuilder.RenameTable(
                name: "Component",
                newName: "Components");

            migrationBuilder.RenameIndex(
                name: "IX_Package_ComponentId",
                table: "Packages",
                newName: "IX_Packages_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_Component_ExpanderId",
                table: "Components",
                newName: "IX_Components_ExpanderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Packages",
                table: "Packages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expanders",
                table: "Expanders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Components",
                table: "Components",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpander_Expanders_ExpandersId",
                table: "AppExpander",
                column: "ExpandersId",
                principalTable: "Expanders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Components_Expanders_ExpanderId",
                table: "Components",
                column: "ExpanderId",
                principalTable: "Expanders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Handler_Expanders_ExpanderId",
                table: "Handler",
                column: "ExpanderId",
                principalTable: "Expanders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Components_ComponentId",
                table: "Packages",
                column: "ComponentId",
                principalTable: "Components",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppExpander_Expanders_ExpandersId",
                table: "AppExpander");

            migrationBuilder.DropForeignKey(
                name: "FK_Components_Expanders_ExpanderId",
                table: "Components");

            migrationBuilder.DropForeignKey(
                name: "FK_Handler_Expanders_ExpanderId",
                table: "Handler");

            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Components_ComponentId",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Packages",
                table: "Packages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Expanders",
                table: "Expanders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Components",
                table: "Components");

            migrationBuilder.RenameTable(
                name: "Packages",
                newName: "Package");

            migrationBuilder.RenameTable(
                name: "Expanders",
                newName: "Expander");

            migrationBuilder.RenameTable(
                name: "Components",
                newName: "Component");

            migrationBuilder.RenameIndex(
                name: "IX_Packages_ComponentId",
                table: "Package",
                newName: "IX_Package_ComponentId");

            migrationBuilder.RenameIndex(
                name: "IX_Components_ExpanderId",
                table: "Component",
                newName: "IX_Component_ExpanderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Package",
                table: "Package",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Expander",
                table: "Expander",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Component",
                table: "Component",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpander_Expander_ExpandersId",
                table: "AppExpander",
                column: "ExpandersId",
                principalTable: "Expander",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Component_Expander_ExpanderId",
                table: "Component",
                column: "ExpanderId",
                principalTable: "Expander",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Handler_Expander_ExpanderId",
                table: "Handler",
                column: "ExpanderId",
                principalTable: "Expander",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Package_Component_ComponentId",
                table: "Package",
                column: "ComponentId",
                principalTable: "Component",
                principalColumn: "Id");
        }
    }
}
