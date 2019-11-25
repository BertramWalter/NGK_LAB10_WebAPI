using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_LAB10_WebAPI.Migrations
{
    public partial class StationClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WeatherStationClient",
                columns: table => new
                {
                    WeatherStationClientId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialNumber = table.Column<string>(maxLength: 16, nullable: true),
                    PwHash = table.Column<string>(maxLength: 60, nullable: true),
                    Password = table.Column<string>(maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherStationClient", x => x.WeatherStationClientId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WeatherStationClient");
        }
    }
}
