using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acxess.Marketing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedPromotionsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                schema: "Marketing",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                schema: "Marketing",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "MinItemsPurchase",
                schema: "Marketing",
                table: "Promotions");

            migrationBuilder.AddColumn<bool>(
                name: "AutoApply",
                schema: "Marketing",
                table: "Promotions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                schema: "Marketing",
                table: "Promotions",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<byte>(
                name: "DiscountType",
                schema: "Marketing",
                table: "Promotions",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AutoApply",
                schema: "Marketing",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "Discount",
                schema: "Marketing",
                table: "Promotions");

            migrationBuilder.DropColumn(
                name: "DiscountType",
                schema: "Marketing",
                table: "Promotions");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                schema: "Marketing",
                table: "Promotions",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                schema: "Marketing",
                table: "Promotions",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinItemsPurchase",
                schema: "Marketing",
                table: "Promotions",
                type: "int",
                nullable: true);
        }
    }
}
