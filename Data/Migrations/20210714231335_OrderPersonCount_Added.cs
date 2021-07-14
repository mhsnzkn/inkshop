using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class OrderPersonCount_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PersonCount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PersonCount",
                table: "Orders");
        }
    }
}
