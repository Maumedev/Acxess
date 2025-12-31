using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acxess.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitCatalog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.CreateTable(
                name: "AccessTiers",
                schema: "Catalog",
                columns: table => new
                {
                    IdAccessTier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessTiers", x => x.IdAccessTier);
                });

            migrationBuilder.CreateTable(
                name: "AddOns",
                schema: "Catalog",
                columns: table => new
                {
                    IdAddOn = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    AddOnKey = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    ShowInCheckout = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddOns", x => x.IdAddOn);
                });

            migrationBuilder.CreateTable(
                name: "PlanAccessTiers",
                schema: "Catalog",
                columns: table => new
                {
                    IdPlanAccessTier = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAccessTier = table.Column<int>(type: "int", nullable: false),
                    IdSellingPlan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanAccessTiers", x => x.IdPlanAccessTier);
                });

            migrationBuilder.CreateTable(
                name: "SellingPlans",
                schema: "Catalog",
                columns: table => new
                {
                    IdSellingPlan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TotalMembers = table.Column<int>(type: "int", nullable: false),
                    DurationInValue = table.Column<byte>(type: "tinyint", nullable: false),
                    DurationUnit = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedByUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellingPlans", x => x.IdSellingPlan);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessTiers_IdTenant",
                schema: "Catalog",
                table: "AccessTiers",
                column: "IdTenant");

            migrationBuilder.CreateIndex(
                name: "IX_AddOns_IdTenant",
                schema: "Catalog",
                table: "AddOns",
                column: "IdTenant");

            migrationBuilder.CreateIndex(
                name: "IX_AddOns_IdTenant_AddOnKey",
                schema: "Catalog",
                table: "AddOns",
                columns: new[] { "IdTenant", "AddOnKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SellingPlans_CreatedByUser",
                schema: "Catalog",
                table: "SellingPlans",
                column: "CreatedByUser");

            migrationBuilder.CreateIndex(
                name: "IX_SellingPlans_IdSellingPlan",
                schema: "Catalog",
                table: "SellingPlans",
                column: "IdSellingPlan");

            migrationBuilder.CreateIndex(
                name: "IX_SellingPlans_IdTenant",
                schema: "Catalog",
                table: "SellingPlans",
                column: "IdTenant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessTiers",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "AddOns",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "PlanAccessTiers",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "SellingPlans",
                schema: "Catalog");
        }
    }
}
