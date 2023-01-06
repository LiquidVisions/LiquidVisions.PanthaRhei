using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class RemovedHandlerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handlers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Handlers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpanderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    SupportedGenerationModes = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Default")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handlers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handlers_Expanders_ExpanderId",
                        column: x => x.ExpanderId,
                        principalTable: "Expanders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Handlers_ExpanderId",
                table: "Handlers",
                column: "ExpanderId");
        }
    }
}
