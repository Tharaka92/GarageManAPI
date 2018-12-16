using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class PartnerPaymentColumnNameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalCashJobs",
                table: "PartnerPayments",
                newName: "CashCount");

            migrationBuilder.RenameColumn(
                name: "TotalCardJobs",
                table: "PartnerPayments",
                newName: "CardCount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CashCount",
                table: "PartnerPayments",
                newName: "TotalCashJobs");

            migrationBuilder.RenameColumn(
                name: "CardCount",
                table: "PartnerPayments",
                newName: "TotalCardJobs");
        }
    }
}
