using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class PaymentColumnsAddedToServiceRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PackagePrice",
                table: "ServiceRequests",
                type: "decimal(15, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TipAmount",
                table: "ServiceRequests",
                type: "decimal(15, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "ServiceRequests",
                type: "decimal(15, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PackagePrice",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "TipAmount",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "ServiceRequests");
        }
    }
}
