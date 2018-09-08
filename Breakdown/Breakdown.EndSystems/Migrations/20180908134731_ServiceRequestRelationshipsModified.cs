using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class ServiceRequestRelationshipsModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleTypeId",
                table: "ServiceRequests",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_CustomerId",
                table: "ServiceRequests",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_PartnerId",
                table: "ServiceRequests",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRequests_VehicleTypeId",
                table: "ServiceRequests",
                column: "VehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_AspNetUsers_CustomerId",
                table: "ServiceRequests",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_AspNetUsers_PartnerId",
                table: "ServiceRequests",
                column: "PartnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceRequests_VehicleTypes_VehicleTypeId",
                table: "ServiceRequests",
                column: "VehicleTypeId",
                principalTable: "VehicleTypes",
                principalColumn: "VehicleTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_AspNetUsers_CustomerId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_AspNetUsers_PartnerId",
                table: "ServiceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceRequests_VehicleTypes_VehicleTypeId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_CustomerId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_PartnerId",
                table: "ServiceRequests");

            migrationBuilder.DropIndex(
                name: "IX_ServiceRequests_VehicleTypeId",
                table: "ServiceRequests");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "ServiceRequests");
        }
    }
}
