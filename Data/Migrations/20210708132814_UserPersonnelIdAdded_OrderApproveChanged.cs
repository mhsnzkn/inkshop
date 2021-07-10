using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class UserPersonnelIdAdded_OrderApproveChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrderApproved",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "IsReservationApproved",
                table: "Orders",
                newName: "IsApproved");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PersonnelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonnelId",
                table: "AspNetUsers",
                column: "PersonnelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Personnel_PersonnelId",
                table: "AspNetUsers",
                column: "PersonnelId",
                principalTable: "Personnel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Personnel_PersonnelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PersonnelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PersonnelId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "Orders",
                newName: "IsReservationApproved");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrderApproved",
                table: "Orders",
                type: "bit",
                nullable: true);
        }
    }
}
