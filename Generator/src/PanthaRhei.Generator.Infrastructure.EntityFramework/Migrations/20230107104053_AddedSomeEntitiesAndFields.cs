using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedSomeEntitiesAndFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsIndex",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsKey",
                table: "Fields",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Relationship",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cardinality = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    WithForeignEntityKeyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WithForeignEntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WithyCardinality = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationship_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationship_Entities_WithForeignEntityId",
                        column: x => x.WithForeignEntityId,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationship_Fields_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationship_Fields_WithForeignEntityKeyId",
                        column: x => x.WithForeignEntityKeyId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_EntityId",
                table: "Relationship",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_KeyId",
                table: "Relationship",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_WithForeignEntityId",
                table: "Relationship",
                column: "WithForeignEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationship_WithForeignEntityKeyId",
                table: "Relationship",
                column: "WithForeignEntityKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relationship");

            migrationBuilder.DropColumn(
                name: "IsIndex",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "IsKey",
                table: "Fields");
        }
    }
}
