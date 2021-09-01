using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class PersonnelCommisionPrecission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Commission",
                table: "Personnel",
                type: "decimal(7,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Commission",
                table: "Personnel",
                type: "decimal(5,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(7,4)");
        }
    }
}
