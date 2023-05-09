using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mb51s",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    ITU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dpyr = table.Column<int>(type: "int", nullable: true),
                    HrkTUMtn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    Customer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mb51s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zm20s",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialSat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Star = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UYCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ArrivalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaterialSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AmountDelivered = table.Column<int>(type: "int", nullable: true),
                    OpenAmount = table.Column<int>(type: "int", nullable: true),
                    RemainingStock = table.Column<int>(type: "int", nullable: true),
                    QualityStock = table.Column<int>(type: "int", nullable: true),
                    SatSasNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    ConfirmationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Mip = table.Column<int>(type: "int", nullable: true),
                    TesMip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SrvRef = table.Column<int>(type: "int", nullable: true),
                    AlanUYEmnStok = table.Column<int>(type: "int", nullable: true),
                    AlanUYYuvDeg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seller = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContractNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Item2 = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zm20s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zs14s",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Star = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstantDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TYeri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MipArea = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Empty1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Definition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HfOrder = table.Column<int>(type: "int", nullable: true),
                    YsOrder = table.Column<int>(type: "int", nullable: true),
                    YDKIOrder = table.Column<int>(type: "int", nullable: true),
                    YDKDOrder = table.Column<int>(type: "int", nullable: true),
                    MIhrTes = table.Column<int>(type: "int", nullable: true),
                    YIIlkSip = table.Column<DateTime>(type: "datetime2", nullable: true),
                    YDIlkSip = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Stnkrz = table.Column<int>(type: "int", nullable: true),
                    CgrSas = table.Column<int>(type: "int", nullable: true),
                    CgrSat = table.Column<int>(type: "int", nullable: true),
                    UYAmbar = table.Column<int>(type: "int", nullable: true),
                    UYDiger = table.Column<int>(type: "int", nullable: true),
                    YP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsSafety = table.Column<int>(type: "int", nullable: true),
                    MP = table.Column<int>(type: "int", nullable: true),
                    Mip = table.Column<int>(type: "int", nullable: true),
                    SG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dr2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SasDelivery = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SasConfirm = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeadlineDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sat = table.Column<int>(type: "int", nullable: true),
                    Sas = table.Column<int>(type: "int", nullable: true),
                    Teslpln = table.Column<int>(type: "int", nullable: true),
                    ConsumptionValue = table.Column<int>(type: "int", nullable: true),
                    TransportStock = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zs14s", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MaterialSKU = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OpenAmount = table.Column<int>(type: "int", nullable: true),
                    Item = table.Column<int>(type: "int", nullable: true),
                    HF = table.Column<int>(type: "int", nullable: true),
                    Urgent = table.Column<int>(type: "int", nullable: true),
                    FirstOrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductClass = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: true),
                    SasCloses = table.Column<int>(type: "int", nullable: true),
                    UrgentCloses = table.Column<int>(type: "int", nullable: true),
                    HfCloses = table.Column<int>(type: "int", nullable: true),
                    ThStock = table.Column<int>(type: "int", nullable: true),
                    Mip = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MipLiable = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sent = table.Column<int>(type: "int", nullable: true),
                    TT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainReports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainReports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OperationClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainReports_UserId",
                table: "MainReports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainReports");

            migrationBuilder.DropTable(
                name: "Mb51s");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "Zm20s");

            migrationBuilder.DropTable(
                name: "Zs14s");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
