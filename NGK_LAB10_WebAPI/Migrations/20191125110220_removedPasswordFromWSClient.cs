using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_LAB10_WebAPI.Migrations
{
    public partial class removedPasswordFromWSClient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "WeatherStationClient");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "WeatherStationClient",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: true);
        }
    }
}
