using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class PlantandRomaniaTablesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Plants",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RomaniaZm20",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    TrmSt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StBu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesInf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item2 = table.Column<int>(type: "int", nullable: true),
                    SaRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item3 = table.Column<int>(type: "int", nullable: true),
                    Orderer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DPYR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UY2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TermQuantity = table.Column<int>(type: "int", nullable: true),
                    Delivered = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenQuantity = table.Column<int>(type: "int", nullable: true),
                    Suply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TesMip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrvRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlanUYEm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlanUYY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Star = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TahKlm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alternative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SasItem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RomaniaZm20", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RomaniaZm20History",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    TrmSt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StBu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalesInf = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item2 = table.Column<int>(type: "int", nullable: true),
                    SaRequest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item3 = table.Column<int>(type: "int", nullable: true),
                    Orderer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DPYR = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UY2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TermQuantity = table.Column<int>(type: "int", nullable: true),
                    Delivered = table.Column<int>(type: "int", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenQuantity = table.Column<int>(type: "int", nullable: true),
                    Suply = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TesMip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cd = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrvRef = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlanUYEm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlanUYY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Star = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TahKlm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ad11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alternative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SasItem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "Date", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RomaniaZm20History", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RomaniaZm20History_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Plants");

            migrationBuilder.DropTable(
                name: "RomaniaZm20");

            migrationBuilder.DropTable(
                name: "RomaniaZm20History");
        }
    }
}
