using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class PartnerPaymentColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardAppFee",
                table: "PartnerPayments");

            migrationBuilder.RenameColumn(
                name: "TotalCashEarning",
                table: "PartnerPayments",
                newName: "AppFeeRemainingAmount");

            migrationBuilder.RenameColumn(
                name: "TotalCardEarning",
                table: "PartnerPayments",
                newName: "AppFeePaidAmount");

            migrationBuilder.RenameColumn(
                name: "CashAppFee",
                table: "PartnerPayments",
                newName: "AppFee");

            migrationBuilder.AlterColumn<int>(
                name: "TotalCashJobs",
                table: "PartnerPayments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TotalCardJobs",
                table: "PartnerPayments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AppFeeRemainingAmount",
                table: "PartnerPayments",
                newName: "TotalCashEarning");

            migrationBuilder.RenameColumn(
                name: "AppFeePaidAmount",
                table: "PartnerPayments",
                newName: "TotalCardEarning");

            migrationBuilder.RenameColumn(
                name: "AppFee",
                table: "PartnerPayments",
                newName: "CashAppFee");

            migrationBuilder.AlterColumn<int>(
                name: "TotalCashJobs",
                table: "PartnerPayments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "TotalCardJobs",
                table: "PartnerPayments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "CardAppFee",
                table: "PartnerPayments",
                type: "decimal(15, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
