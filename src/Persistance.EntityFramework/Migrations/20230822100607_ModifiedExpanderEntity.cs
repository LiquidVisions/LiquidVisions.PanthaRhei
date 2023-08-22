using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedExpanderEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TemplateFolder",
                table: "Expanders");

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "Expanders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "Expanders");

            migrationBuilder.AddColumn<string>(
                name: "TemplateFolder",
                table: "Expanders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
