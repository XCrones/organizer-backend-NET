using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace organizer_backend_NET.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWeatherForecastTypeCodRmCodMessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cod",
                table: "WeatherForecasts");

            migrationBuilder.DropColumn(
                name: "message",
                table: "WeatherForecasts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cod",
                table: "WeatherForecasts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "message",
                table: "WeatherForecasts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
