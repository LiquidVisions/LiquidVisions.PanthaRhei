using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedTypeBehaviourAndModifierToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Behaviour",
                table: "Entities",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Modifier",
                table: "Entities",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "public");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Entities",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: false,
                defaultValue: "class");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Behaviour",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Modifier",
                table: "Entities");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Entities");
        }
    }
}
