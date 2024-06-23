using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemoveComponentReferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComponentReferences");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ComponentReferences",
                columns: table => new
                {
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReferencedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComponentReferences", x => new { x.ComponentId, x.ReferencedById });
                    table.ForeignKey(
                        name: "FK_ComponentReferences_Component",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComponentReferences_ReferencedBy",
                        column: x => x.ReferencedById,
                        principalTable: "Components",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComponentReferences_ReferencedById",
                table: "ComponentReferences",
                column: "ReferencedById");
        }
    }
}
