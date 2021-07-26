using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class OrderPersonnel_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Personnel_PersonnelId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_PersonnelId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderPersonnel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    PersonnelId = table.Column<int>(type: "int", nullable: false),
                    Job = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPersonnel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPersonnel_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPersonnel_Personnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderPersonnel_OrderId",
                table: "OrderPersonnel",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPersonnel_PersonnelId",
                table: "OrderPersonnel",
                column: "PersonnelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderPersonnel");

            migrationBuilder.AddColumn<int>(
                name: "PersonnelId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PersonnelId",
                table: "Orders",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Personnel_PersonnelId",
                table: "Orders",
                column: "PersonnelId",
                principalTable: "Personnel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
