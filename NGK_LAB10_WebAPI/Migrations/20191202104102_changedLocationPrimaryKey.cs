using Microsoft.EntityFrameworkCore.Migrations;

namespace NGK_LAB10_WebAPI.Migrations
{
    public partial class changedLocationPrimaryKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherObservation_Location_LocationName",
                table: "WeatherObservation");

            migrationBuilder.DropIndex(
                name: "IX_WeatherObservation_LocationName",
                table: "WeatherObservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LocationName",
                table: "WeatherObservation");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "WeatherObservation",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Location",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Location",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherObservation_LocationId",
                table: "WeatherObservation",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherObservation_Location_LocationId",
                table: "WeatherObservation",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WeatherObservation_Location_LocationId",
                table: "WeatherObservation");

            migrationBuilder.DropIndex(
                name: "IX_WeatherObservation_LocationId",
                table: "WeatherObservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "WeatherObservation");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Location");

            migrationBuilder.AddColumn<string>(
                name: "LocationName",
                table: "WeatherObservation",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Location",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_WeatherObservation_LocationName",
                table: "WeatherObservation",
                column: "LocationName");

            migrationBuilder.AddForeignKey(
                name: "FK_WeatherObservation_Location_LocationName",
                table: "WeatherObservation",
                column: "LocationName",
                principalTable: "Location",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
