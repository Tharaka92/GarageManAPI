using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class PartnerPaymentsEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PartnerPayments",
                columns: table => new
                {
                    PartnerPaymentId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PartnerId = table.Column<int>(nullable: false),
                    TotalCashJobs = table.Column<int>(nullable: true),
                    TotalCardJobs = table.Column<int>(nullable: true),
                    From = table.Column<DateTime>(nullable: true),
                    To = table.Column<DateTime>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    TotalCashAmount = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    TotalCardAmount = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    CashAppFee = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    CardAppFee = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    TotalCardEarning = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    TotalCashEarning = table.Column<decimal>(type: "decimal(15, 2)", nullable: false),
                    HasPaid = table.Column<bool>(nullable: false),
                    HasReceived = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartnerPayments", x => x.PartnerPaymentId);
                    table.ForeignKey(
                        name: "FK_PartnerPayments_AspNetUsers_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PartnerPayments_PartnerId",
                table: "PartnerPayments",
                column: "PartnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartnerPayments");
        }
    }
}
