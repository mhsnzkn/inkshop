using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CancellationReason_Fixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderCancellationReason",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "ReservationCancellationReason",
                table: "Orders",
                newName: "CancellationReason");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CancellationReason",
                table: "Orders",
                newName: "ReservationCancellationReason");

            migrationBuilder.AddColumn<string>(
                name: "OrderCancellationReason",
                table: "Orders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
