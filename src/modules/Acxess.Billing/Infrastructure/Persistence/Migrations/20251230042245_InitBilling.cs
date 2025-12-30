using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acxess.Billing.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitBilling : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Billing");

            migrationBuilder.CreateTable(
                name: "MemberTransactionDetails",
                schema: "Billing",
                columns: table => new
                {
                    IdMemberTransactionDetail = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdMemberTransaction = table.Column<int>(type: "int", nullable: false),
                    IdSubscription = table.Column<int>(type: "int", nullable: true),
                    IdItem = table.Column<int>(type: "int", nullable: true),
                    ItemTransactionType = table.Column<byte>(type: "tinyint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTransactionDetails", x => x.IdMemberTransactionDetail);
                });

            migrationBuilder.CreateTable(
                name: "MemberTransactions",
                schema: "Billing",
                columns: table => new
                {
                    IdMemberTransaction = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    IdMember = table.Column<int>(type: "int", nullable: true),
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedByUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTransactions", x => x.IdMemberTransaction);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                schema: "Billing",
                columns: table => new
                {
                    IdPaymentMethod = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTenant = table.Column<int>(type: "int", nullable: true),
                    Method = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.IdPaymentMethod);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberTransactionDetails_IdMemberTransaction",
                schema: "Billing",
                table: "MemberTransactionDetails",
                column: "IdMemberTransaction");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTransactions_IdMember",
                schema: "Billing",
                table: "MemberTransactions",
                column: "IdMember");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTransactions_IdTenant",
                schema: "Billing",
                table: "MemberTransactions",
                column: "IdTenant");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberTransactionDetails",
                schema: "Billing");

            migrationBuilder.DropTable(
                name: "MemberTransactions",
                schema: "Billing");

            migrationBuilder.DropTable(
                name: "PaymentMethods",
                schema: "Billing");
        }
    }
}
