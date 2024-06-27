    using Microsoft.EntityFrameworkCore.Migrations;

    #nullable disable

    namespace TourManagementSystem.Migrations
    {
        public partial class AddServiceTable : Migration
        {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.CreateTable(
                    name: "Services",
                    columns: table => new
                    {
                        Id = table.Column<int>(type: "INTEGER", nullable: false)
                            .Annotation("Sqlite:Autoincrement", true),
                        NumberOfPeople = table.Column<string>(type: "TEXT", nullable: false),
                        NumberOfDays = table.Column<string>(type: "TEXT", nullable: false),
                        IsRequiredPersonalGuide = table.Column<bool>(type: "INTEGER", nullable: false),
                        NoOfRoom = table.Column<string>(type: "TEXT", nullable: true),
                        TourType = table.Column<string>(type: "TEXT", nullable: false),
                        Description = table.Column<string>(type: "TEXT", nullable: true),
                        DestinationId = table.Column<int>(type: "INTEGER", nullable: false),
                        UserId = table.Column<int>(type: "INTEGER", nullable: false)
                    },
                    constraints: table =>
                    {
                        table.PrimaryKey("PK_Services", x => x.Id);
                        table.ForeignKey(
                            name: "FK_Services_Destinations_DestinationId",
                            column: x => x.DestinationId,
                            principalTable: "Destinations",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                        table.ForeignKey(
                            name: "FK_Services_Users_UserId",
                            column: x => x.UserId,
                            principalTable: "Users",
                            principalColumn: "Id",
                            onDelete: ReferentialAction.Cascade);
                    });

                migrationBuilder.CreateIndex(
                    name: "IX_Services_DestinationId",
                    table: "Services",
                    column: "DestinationId");

                migrationBuilder.CreateIndex(
                    name: "IX_Services_UserId",
                    table: "Services",
                    column: "UserId");
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropTable(
                    name: "Services");
            }
        }
    }
