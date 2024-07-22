using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourManagementSystem.Migrations
{
    public partial class AddAttributesInBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings");

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "Bookings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "contact",
                table: "Bookings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "userName",
                table: "Bookings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BookingDetailsDTO",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TourDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    DestinationName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingDetailsDTO", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingDetailsDTO");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "contact",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "userName",
                table: "Bookings");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserId",
                table: "Bookings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Users_UserId",
                table: "Bookings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
