using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class OrderTicketNumber_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketNumber",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketNumber",
                table: "Orders");
        }
    }
}
