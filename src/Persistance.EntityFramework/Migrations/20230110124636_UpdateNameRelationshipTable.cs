using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNameRelationshipTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationship_Entities_EntityId",
                table: "Relationship");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationship_Entities_WithForeignEntityId",
                table: "Relationship");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationship_Fields_KeyId",
                table: "Relationship");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationship_Fields_WithForeignEntityKeyId",
                table: "Relationship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relationship",
                table: "Relationship");

            migrationBuilder.RenameTable(
                name: "Relationship",
                newName: "Relationships");

            migrationBuilder.RenameIndex(
                name: "IX_Relationship_WithForeignEntityKeyId",
                table: "Relationships",
                newName: "IX_Relationships_WithForeignEntityKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationship_WithForeignEntityId",
                table: "Relationships",
                newName: "IX_Relationships_WithForeignEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationship_KeyId",
                table: "Relationships",
                newName: "IX_Relationships_KeyId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationship_EntityId",
                table: "Relationships",
                newName: "IX_Relationships_EntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relationships",
                table: "Relationships",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Entities_EntityId",
                table: "Relationships",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Entities_WithForeignEntityId",
                table: "Relationships",
                column: "WithForeignEntityId",
                principalTable: "Entities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Fields_KeyId",
                table: "Relationships",
                column: "KeyId",
                principalTable: "Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationships_Fields_WithForeignEntityKeyId",
                table: "Relationships",
                column: "WithForeignEntityKeyId",
                principalTable: "Fields",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Entities_EntityId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Entities_WithForeignEntityId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Fields_KeyId",
                table: "Relationships");

            migrationBuilder.DropForeignKey(
                name: "FK_Relationships_Fields_WithForeignEntityKeyId",
                table: "Relationships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Relationships",
                table: "Relationships");

            migrationBuilder.RenameTable(
                name: "Relationships",
                newName: "Relationship");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_WithForeignEntityKeyId",
                table: "Relationship",
                newName: "IX_Relationship_WithForeignEntityKeyId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_WithForeignEntityId",
                table: "Relationship",
                newName: "IX_Relationship_WithForeignEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_KeyId",
                table: "Relationship",
                newName: "IX_Relationship_KeyId");

            migrationBuilder.RenameIndex(
                name: "IX_Relationships_EntityId",
                table: "Relationship",
                newName: "IX_Relationship_EntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relationship",
                table: "Relationship",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationship_Entities_EntityId",
                table: "Relationship",
                column: "EntityId",
                principalTable: "Entities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationship_Entities_WithForeignEntityId",
                table: "Relationship",
                column: "WithForeignEntityId",
                principalTable: "Entities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationship_Fields_KeyId",
                table: "Relationship",
                column: "KeyId",
                principalTable: "Fields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Relationship_Fields_WithForeignEntityKeyId",
                table: "Relationship",
                column: "WithForeignEntityKeyId",
                principalTable: "Fields",
                principalColumn: "Id");
        }
    }
}
