using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acxess.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ForeignKeySellingPlans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdTenant",
                schema: "Catalog",
                table: "PlanAccessTiers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PlanAccessTiers_IdAccessTier",
                schema: "Catalog",
                table: "PlanAccessTiers",
                column: "IdAccessTier");

            migrationBuilder.CreateIndex(
                name: "IX_PlanAccessTiers_IdSellingPlan",
                schema: "Catalog",
                table: "PlanAccessTiers",
                column: "IdSellingPlan");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAccessTiers_AccessTiers_IdAccessTier",
                schema: "Catalog",
                table: "PlanAccessTiers",
                column: "IdAccessTier",
                principalSchema: "Catalog",
                principalTable: "AccessTiers",
                principalColumn: "IdAccessTier",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanAccessTiers_SellingPlans_IdSellingPlan",
                schema: "Catalog",
                table: "PlanAccessTiers",
                column: "IdSellingPlan",
                principalSchema: "Catalog",
                principalTable: "SellingPlans",
                principalColumn: "IdSellingPlan",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanAccessTiers_AccessTiers_IdAccessTier",
                schema: "Catalog",
                table: "PlanAccessTiers");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanAccessTiers_SellingPlans_IdSellingPlan",
                schema: "Catalog",
                table: "PlanAccessTiers");

            migrationBuilder.DropIndex(
                name: "IX_PlanAccessTiers_IdAccessTier",
                schema: "Catalog",
                table: "PlanAccessTiers");

            migrationBuilder.DropIndex(
                name: "IX_PlanAccessTiers_IdSellingPlan",
                schema: "Catalog",
                table: "PlanAccessTiers");

            migrationBuilder.DropColumn(
                name: "IdTenant",
                schema: "Catalog",
                table: "PlanAccessTiers");
        }
    }
}
