using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiquidVisions.PanthaRhei.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigrationTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expanders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TemplateFolder = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expanders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionStrings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionStrings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionStrings_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Entities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Callsite = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false, defaultValue: "class"),
                    Modifier = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValue: "public"),
                    Behaviour = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    AppId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Entities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Entities_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        name: "FK_AppExpander_Apps_AppsId",
                        column: x => x.AppsId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppExpander_Expanders_ExpandersId",
                        column: x => x.ExpandersId,
                        principalTable: "Expanders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Components",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2056)", maxLength: 2056, nullable: true),
                    ExpanderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Components", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Components_Expanders_ExpanderId",
                        column: x => x.ExpanderId,
                        principalTable: "Expanders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ReturnType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCollection = table.Column<bool>(type: "bit", nullable: false),
                    Modifier = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false, defaultValue: "public"),
                    GetModifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SetModifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Behaviour = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    Order = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Required = table.Column<bool>(type: "bit", nullable: false),
                    ReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    EntityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsKey = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsIndex = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fields_Entities_ReferenceId",
                        column: x => x.ReferenceId,
                        principalTable: "Entities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Packages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Packages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Packages_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Relationships",
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
                    table.PrimaryKey("PK_Relationships", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Relationships_Entities_EntityId",
                        column: x => x.EntityId,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationships_Entities_WithForeignEntityId",
                        column: x => x.WithForeignEntityId,
                        principalTable: "Entities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationships_Fields_KeyId",
                        column: x => x.KeyId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Relationships_Fields_WithForeignEntityKeyId",
                        column: x => x.WithForeignEntityKeyId,
                        principalTable: "Fields",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppExpander_ExpandersId",
                table: "AppExpander",
                column: "ExpandersId");

            migrationBuilder.CreateIndex(
                name: "IX_Components_ExpanderId",
                table: "Components",
                column: "ExpanderId");

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionStrings_AppId",
                table: "ConnectionStrings",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Entities_AppId",
                table: "Entities",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_EntityId",
                table: "Fields",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_ReferenceId",
                table: "Fields",
                column: "ReferenceId");

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ComponentId",
                table: "Packages",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_EntityId",
                table: "Relationships",
                column: "EntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_KeyId",
                table: "Relationships",
                column: "KeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_WithForeignEntityId",
                table: "Relationships",
                column: "WithForeignEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_WithForeignEntityKeyId",
                table: "Relationships",
                column: "WithForeignEntityKeyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppExpander");

            migrationBuilder.DropTable(
                name: "ConnectionStrings");

            migrationBuilder.DropTable(
                name: "Packages");

            migrationBuilder.DropTable(
                name: "Relationships");

            migrationBuilder.DropTable(
                name: "Components");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Expanders");

            migrationBuilder.DropTable(
                name: "Entities");

            migrationBuilder.DropTable(
                name: "Apps");
        }
    }
}
