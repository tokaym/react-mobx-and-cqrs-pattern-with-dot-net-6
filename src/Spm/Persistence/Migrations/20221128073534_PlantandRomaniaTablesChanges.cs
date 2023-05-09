using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class PlantandRomaniaTablesChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RomaniaZm20History_Users_UserId",
                table: "RomaniaZm20History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RomaniaZm20History",
                table: "RomaniaZm20History");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RomaniaZm20",
                table: "RomaniaZm20");

            migrationBuilder.RenameTable(
                name: "RomaniaZm20History",
                newName: "RomaniaZm20Histories");

            migrationBuilder.RenameTable(
                name: "RomaniaZm20",
                newName: "RomaniaZm20s");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RomaniaZm20Histories",
                table: "RomaniaZm20Histories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RomaniaZm20s",
                table: "RomaniaZm20s",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RomaniaZm20Histories_Users_UserId",
                table: "RomaniaZm20Histories",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RomaniaZm20Histories_Users_UserId",
                table: "RomaniaZm20Histories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RomaniaZm20s",
                table: "RomaniaZm20s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RomaniaZm20Histories",
                table: "RomaniaZm20Histories");

            migrationBuilder.RenameTable(
                name: "RomaniaZm20s",
                newName: "RomaniaZm20");

            migrationBuilder.RenameTable(
                name: "RomaniaZm20Histories",
                newName: "RomaniaZm20History");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RomaniaZm20",
                table: "RomaniaZm20",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RomaniaZm20History",
                table: "RomaniaZm20History",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RomaniaZm20History_Users_UserId",
                table: "RomaniaZm20History",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
