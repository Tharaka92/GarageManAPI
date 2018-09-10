using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class ServiceRequestTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "ServiceRequests",
                newName: "ServiceRequestStatus");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ServiceRequests",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PaymentStatus",
                table: "ServiceRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "PaymentStatus",
                table: "ServiceRequests");

            migrationBuilder.RenameColumn(
                name: "ServiceRequestStatus",
                table: "ServiceRequests",
                newName: "Status");
        }
    }
}
