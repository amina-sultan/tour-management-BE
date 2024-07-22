using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourManagementSystem.Migrations
{
    public partial class AddImageAttributeInDestination : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "BookingDetailsDTO",
                newName: "userName");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Destinations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "userName",
                table: "BookingDetailsDTO",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "CNIC",
                table: "BookingDetailsDTO",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "contact",
                table: "BookingDetailsDTO",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "CNIC",
                table: "BookingDetailsDTO");

            migrationBuilder.DropColumn(
                name: "contact",
                table: "BookingDetailsDTO");

            migrationBuilder.RenameColumn(
                name: "userName",
                table: "BookingDetailsDTO",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "BookingDetailsDTO",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
