using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Generated.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class PanthaRheiGeneratedGenerated638191429306477432 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "App",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_App", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expander",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateFolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expander", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionString",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionString", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionString_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Entity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Callsite = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Behaviour = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entity_App_AppId",
                        column: x => x.AppId,
                        principalTable: "App",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppExpander",
                columns: table => new
                {
                    AppsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExpandersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppExpander", x => new { x.AppsId, x.ExpandersId });
                    table.ForeignKey(
                        name: "FK_AppExpander_App_AppsId",
                        column: x => x.AppsId,
                        principalTable: "App",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppExpander_Expander_ExpandersId",
                        column: x => x.ExpandersId,
                        principalTable: "Expander",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Component",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2056)", maxLength: 2056, nullable: true),
                    ExpanderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Component", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Component_Expander_ExpanderId",
                        column: x => x.ExpanderId,
                        principalTable: "Expander",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Field",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ReturnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCollection = table.Column<bool>(type: "bit", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    GetModifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetModifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Behaviour = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsKey = table.Column<bool>(type: "bit", nullable: false),
                    IsIndex = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Field", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Field_Entity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Field_Entity_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "Entity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Package",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Package", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Package_Component_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Component",
                        principalColumn: "Id");
                });

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
                    WithCardinality = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Required = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationship", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationship_Entity_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationship_Entity_WithForeignEntityId",
                        column: x => x.WithForeignEntityId,
                        principalTable: "Entity",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationship_Field_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Field",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationship_Field_WithForeignEntityKeyId",
                        column: x => x.WithForeignEntityKeyId,
                        principalTable: "Field",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppExpander_ExpandersId",
                table: "AppExpander",
                column: "ExpandersId");

            migrationBuilder.CreateIndex(
                name: "IX_Component_ExpanderId",
                table: "Component",
                column: "ExpanderId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionString_AppId",
                table: "ConnectionString",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Entity_AppId",
                table: "Entity",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_EntityId",
                table: "Field",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Field_ReferenceId",
                table: "Field",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Package_ComponentId",
                table: "Package",
                column: "ComponentId");

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
                name: "AppExpander");

            migrationBuilder.DropTable(
                name: "ConnectionString");

            migrationBuilder.DropTable(
                name: "Package");

            migrationBuilder.DropTable(
                name: "Relationship");

            migrationBuilder.DropTable(
                name: "Component");

            migrationBuilder.DropTable(
                name: "Field");

            migrationBuilder.DropTable(
                name: "Expander");

            migrationBuilder.DropTable(
                name: "Entity");

            migrationBuilder.DropTable(
                name: "App");
        }
    }
}
