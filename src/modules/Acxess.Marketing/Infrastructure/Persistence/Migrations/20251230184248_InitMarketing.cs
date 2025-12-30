using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acxess.Marketing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitMarketing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Marketing");

            migrationBuilder.CreateTable(
                name: "AppliedPromotions",
                schema: "Marketing",
                columns: table => new
                {
                    IdAppliedPromotion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMemberTransactionDetail = table.Column<int>(type: "int", nullable: false),
                    IdPromotion = table.Column<int>(type: "int", nullable: true),
                    IdCoupon = table.Column<int>(type: "int", nullable: true),
                    AppliedAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppliedPromotions", x => x.IdAppliedPromotion);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                schema: "Marketing",
                columns: table => new
                {
                    IdCoupon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    IdMember = table.Column<int>(type: "int", nullable: false),
                    IdPromotion = table.Column<int>(type: "int", nullable: false),
                    IsRedeemed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedByUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.IdCoupon);
                });

            migrationBuilder.CreateTable(
                name: "Promotions",
                schema: "Marketing",
                columns: table => new
                {
                    IdPromotion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    RequiresCoupon = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    MinItemsPurchase = table.Column<int>(type: "int", nullable: true),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AvailableTo = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedByUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promotions", x => x.IdPromotion);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppliedPromotions",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Coupons",
                schema: "Marketing");

            migrationBuilder.DropTable(
                name: "Promotions",
                schema: "Marketing");
        }
    }
}
