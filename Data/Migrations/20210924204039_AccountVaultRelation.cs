using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class AccountVaultRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VaultInId",
                table: "AccountMovements",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VaultOutId",
                table: "AccountMovements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovements_VaultInId",
                table: "AccountMovements",
                column: "VaultInId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovements_VaultOutId",
                table: "AccountMovements",
                column: "VaultOutId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_AccountVaults_VaultInId",
                table: "AccountMovements",
                column: "VaultInId",
                principalTable: "AccountVaults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_AccountVaults_VaultOutId",
                table: "AccountMovements",
                column: "VaultOutId",
                principalTable: "AccountVaults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_AccountVaults_VaultInId",
                table: "AccountMovements");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_AccountVaults_VaultOutId",
                table: "AccountMovements");

            migrationBuilder.DropIndex(
                name: "IX_AccountMovements_VaultInId",
                table: "AccountMovements");

            migrationBuilder.DropIndex(
                name: "IX_AccountMovements_VaultOutId",
                table: "AccountMovements");

            migrationBuilder.DropColumn(
                name: "VaultInId",
                table: "AccountMovements");

            migrationBuilder.DropColumn(
                name: "VaultOutId",
                table: "AccountMovements");
        }
    }
}
