using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acxess.Membership.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitMembership : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Membership");

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "Membership",
                columns: table => new
                {
                    IdMember = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    FirtsName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedByUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.IdMember);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionAddOns",
                schema: "Membership",
                columns: table => new
                {
                    IdSubscriptionAddOn = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAddOn = table.Column<int>(type: "int", nullable: false),
                    IdSubscription = table.Column<int>(type: "int", nullable: false),
                    PriceSnapshot = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionAddOns", x => x.IdSubscriptionAddOn);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionMembers",
                schema: "Membership",
                columns: table => new
                {
                    IdSubscriptionMember = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMember = table.Column<int>(type: "int", nullable: false),
                    IdSubscription = table.Column<int>(type: "int", nullable: false),
                    Owner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionMembers", x => x.IdSubscriptionMember);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                schema: "Membership",
                columns: table => new
                {
                    IdSubscription = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    IdMemberOwner = table.Column<int>(type: "int", nullable: false),
                    IdSellingPlan = table.Column<int>(type: "int", nullable: false),
                    IsAcive = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceSnapshot = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedByUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.IdSubscription);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_IdMember",
                schema: "Membership",
                table: "Members",
                column: "IdMember");

            migrationBuilder.CreateIndex(
                name: "IX_Members_IdTenant",
                schema: "Membership",
                table: "Members",
                column: "IdTenant");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_IdTenant",
                schema: "Membership",
                table: "Subscriptions",
                column: "IdTenant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Members",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "SubscriptionAddOns",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "SubscriptionMembers",
                schema: "Membership");

            migrationBuilder.DropTable(
                name: "Subscriptions",
                schema: "Membership");
        }
    }
}
