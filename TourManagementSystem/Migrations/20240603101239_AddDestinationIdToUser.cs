using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourManagementSystem.Migrations
{
    public partial class AddDestinationIdToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_DestinationId",
                table: "Users",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Destinations_DestinationId",
                table: "Users",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Destinations_DestinationId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_DestinationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "Users");
        }
    }
}
