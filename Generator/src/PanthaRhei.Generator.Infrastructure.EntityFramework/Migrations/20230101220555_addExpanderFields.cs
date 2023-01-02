using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class addExpanderFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("SqlServer:EditionOptions", "EDITION = 'Basic'");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Expander",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TemplateFolder",
                table: "Expander",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Handler",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    ExpanderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SupportedGenerationModes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handler_Expander_ExpanderId",
                        column: x => x.ExpanderId,
                        principalTable: "Expander",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Handler_ExpanderId",
                table: "Handler",
                column: "ExpanderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Handler");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Expander");

            migrationBuilder.DropColumn(
                name: "TemplateFolder",
                table: "Expander");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("SqlServer:EditionOptions", "EDITION = 'Basic'");
        }
    }
}
