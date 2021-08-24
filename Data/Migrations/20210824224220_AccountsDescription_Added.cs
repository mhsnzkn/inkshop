using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AccountsDescription_Added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AccountTypes",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CrtDate",
                table: "AccountEntities",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AccountEntities",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "AccountEntities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UptDate",
                table: "AccountEntities",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AccountTypes");

            migrationBuilder.DropColumn(
                name: "CrtDate",
                table: "AccountEntities");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "AccountEntities");

            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "AccountEntities");

            migrationBuilder.DropColumn(
                name: "UptDate",
                table: "AccountEntities");
        }
    }
}
