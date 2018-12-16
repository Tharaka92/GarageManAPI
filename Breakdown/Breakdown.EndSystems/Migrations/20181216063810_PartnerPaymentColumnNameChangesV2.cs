using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class PartnerPaymentColumnNameChangesV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "To",
                table: "PartnerPayments",
                newName: "ToDate");

            migrationBuilder.RenameColumn(
                name: "From",
                table: "PartnerPayments",
                newName: "FromDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ToDate",
                table: "PartnerPayments",
                newName: "To");

            migrationBuilder.RenameColumn(
                name: "FromDate",
                table: "PartnerPayments",
                newName: "From");
        }
    }
}
