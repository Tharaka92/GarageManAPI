using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class PaymentTypeColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "ServiceRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "ServiceRequests");
        }
    }
}
