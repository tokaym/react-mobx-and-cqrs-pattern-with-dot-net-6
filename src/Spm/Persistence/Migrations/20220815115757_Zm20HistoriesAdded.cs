using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Zm20HistoriesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ReportDate",
                table: "MainReports",
                type: "Date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "MainReports",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Zm20Histories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialSat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Star = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UYCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DY = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MaterialSKU = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AmountDelivered = table.Column<int>(type: "int", nullable: false),
                    OpenAmount = table.Column<int>(type: "int", nullable: false),
                    RemainingStock = table.Column<int>(type: "int", nullable: false),
                    QualityStock = table.Column<int>(type: "int", nullable: false),
                    SatSasNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item = table.Column<int>(type: "int", nullable: false),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Mip = table.Column<int>(type: "int", nullable: false),
                    TesMip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SrvRef = table.Column<int>(type: "int", nullable: false),
                    AlanUYEmnStok = table.Column<int>(type: "int", nullable: false),
                    AlanUYYuvDeg = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty6 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty7 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty8 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContractNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Empty9 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Item2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "Date", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zm20Histories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zm20Histories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Zm20Histories_UserId",
                table: "Zm20Histories",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Zm20Histories");

            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "MainReports");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReportDate",
                table: "MainReports",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "Date");
        }
    }
}
