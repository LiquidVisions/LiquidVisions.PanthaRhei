using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedCallsiteToEntityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Callsite",
                table: "Entities",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Callsite",
                table: "Entities");
        }
    }
}
