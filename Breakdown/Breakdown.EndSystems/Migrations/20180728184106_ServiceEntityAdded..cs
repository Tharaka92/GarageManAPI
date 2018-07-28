using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Breakdown.EndSystems.Migrations
{
    public partial class ServiceEntityAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Packages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ServiceName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.ServiceId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_ServiceId",
                table: "Packages",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Packages_Services_ServiceId",
                table: "Packages",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "ServiceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Packages_Services_ServiceId",
                table: "Packages");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Packages_ServiceId",
                table: "Packages");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Packages");
        }
    }
}
