using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AccountsFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "AccountEntities");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "AccountEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
