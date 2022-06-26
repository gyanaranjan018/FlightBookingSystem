using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingService.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FlightDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Meals = table.Column<short>(type: "smallint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AirlineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoOfSeats = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    FlightType = table.Column<int>(type: "int", nullable: false),
                    OutBoundId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CouponCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountPercent = table.Column<int>(type: "int", nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_FlightDetails_OutBoundId",
                        column: x => x.OutBoundId,
                        principalTable: "FlightDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_FlightDetails_ReturnId",
                        column: x => x.ReturnId,
                        principalTable: "FlightDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookingDetails_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingDetails_BookingId",
                table: "BookingDetails",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OutBoundId",
                table: "Bookings",
                column: "OutBoundId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ReturnId",
                table: "Bookings",
                column: "ReturnId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetails");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "FlightDetails");
        }
    }
}
