using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Acxess.Identity.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsTenants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tenants_IdTenant",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdTenant",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserNumber",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdTenant",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_UserNumber",
                schema: "Identity",
                table: "AspNetUsers",
                column: "UserNumber");

            migrationBuilder.CreateTable(
                name: "TenantsUsers",
                schema: "Identity",
                columns: table => new
                {
                    IdTenant = table.Column<int>(type: "int", nullable: false),
                    UserNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenantsUsers", x => new { x.IdTenant, x.UserNumber });
                    table.ForeignKey(
                        name: "FK_TenantsUsers_AspNetUsers_UserNumber",
                        column: x => x.UserNumber,
                        principalSchema: "Identity",
                        principalTable: "AspNetUsers",
                        principalColumn: "UserNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TenantsUsers_Tenants_IdTenant",
                        column: x => x.IdTenant,
                        principalSchema: "Identity",
                        principalTable: "Tenants",
                        principalColumn: "IdTenant",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TenantsUsers_UserNumber",
                schema: "Identity",
                table: "TenantsUsers",
                column: "UserNumber");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TenantsUsers",
                schema: "Identity");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_UserNumber",
                schema: "Identity",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "IdTenant",
                schema: "Identity",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdTenant",
                schema: "Identity",
                table: "AspNetUsers",
                column: "IdTenant");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserNumber",
                schema: "Identity",
                table: "AspNetUsers",
                column: "UserNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tenants_IdTenant",
                schema: "Identity",
                table: "AspNetUsers",
                column: "IdTenant",
                principalSchema: "Identity",
                principalTable: "Tenants",
                principalColumn: "IdTenant",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
