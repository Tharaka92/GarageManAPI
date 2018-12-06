using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class TipAmountRenamedToPartnerAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipAmount",
                table: "ServiceRequests",
                newName: "PartnerAmount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartnerAmount",
                table: "ServiceRequests",
                newName: "TipAmount");
        }
    }
}
