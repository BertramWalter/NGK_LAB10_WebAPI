using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_LAB10_WebAPI.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "WeatherObservation",
                columns: table => new
                {
                    WeatherObservationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    TemperatureC = table.Column<decimal>(type: "decimal(10,1)", nullable: false),
                    LocationName = table.Column<string>(nullable: true),
                    Humidity = table.Column<int>(nullable: false),
                    AirPressure = table.Column<decimal>(type: "decimal(10,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherObservation", x => x.WeatherObservationId);
                    table.ForeignKey(
                        name: "FK_WeatherObservation_Location_LocationName",
                        column: x => x.LocationName,
                        principalTable: "Location",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WeatherObservation_LocationName",
                table: "WeatherObservation",
                column: "LocationName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherObservation");

            migrationBuilder.DropTable(
                name: "Location");
        }
    }
}
