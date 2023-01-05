using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LiquidVisions.PanthaRhei.Generator.Infrastructure.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpanderComponentRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("20c2975d-f693-4280-ba1f-fc60727c6911"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("60cbbc7a-4bf5-41be-aeda-e9bbfa2f2764"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("931b2845-6d46-4ac0-8316-99c90511ad85"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("a8945343-f1a0-465f-b201-5cac4a2ce64c"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("bc647b63-35ee-4527-867c-2114ab41cd8d"), "EntityType" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("1745e4d1-62e1-4d29-8762-1c75df2c2aec"), "Keyword", "abstract" },
                    { new Guid("50e03285-3663-4f0e-ad6e-19fbd30edbe2"), "EntityType", "class" },
                    { new Guid("58132b56-77f7-443a-8137-ce05255136d4"), "EntityType", "enum" },
                    { new Guid("90127df5-74b7-4d6d-9605-cdcc2348eb27"), "Keyword", "override" },
                    { new Guid("b3971f10-f3bd-448f-ad44-91c7d7db647f"), "EntityType", "interface" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("1745e4d1-62e1-4d29-8762-1c75df2c2aec"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("50e03285-3663-4f0e-ad6e-19fbd30edbe2"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("58132b56-77f7-443a-8137-ce05255136d4"), "EntityType" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("90127df5-74b7-4d6d-9605-cdcc2348eb27"), "Keyword" });

            migrationBuilder.DeleteData(
                table: "Options",
                keyColumns: new[] { "Id", "Key" },
                keyValues: new object[] { new Guid("b3971f10-f3bd-448f-ad44-91c7d7db647f"), "EntityType" });

            migrationBuilder.InsertData(
                table: "Options",
                columns: new[] { "Id", "Key", "Value" },
                values: new object[,]
                {
                    { new Guid("20c2975d-f693-4280-ba1f-fc60727c6911"), "EntityType", "enum" },
                    { new Guid("60cbbc7a-4bf5-41be-aeda-e9bbfa2f2764"), "EntityType", "interface" },
                    { new Guid("931b2845-6d46-4ac0-8316-99c90511ad85"), "Keyword", "abstract" },
                    { new Guid("a8945343-f1a0-465f-b201-5cac4a2ce64c"), "Keyword", "override" },
                    { new Guid("bc647b63-35ee-4527-867c-2114ab41cd8d"), "EntityType", "class" }
                });
        }
    }
}
