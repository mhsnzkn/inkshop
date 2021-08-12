using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class OrderTypeDataSeedPersonnelCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Personnel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "CrtDate", "Name", "UptDate" },
                values: new object[] { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "Dövme", null });

            migrationBuilder.InsertData(
                table: "OrderTypes",
                columns: new[] { "Id", "CrtDate", "Name", "UptDate" },
                values: new object[] { 2, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Local), "Make Piercing", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Category",
                table: "Personnel");
        }
    }
}
