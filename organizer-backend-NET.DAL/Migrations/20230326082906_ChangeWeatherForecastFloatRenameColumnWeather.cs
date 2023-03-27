using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace organizer_backend_NET.DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWeatherForecastFloatRenameColumnWeather : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "weather",
                table: "WeatherForecasts",
                newName: "list");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "list",
                table: "WeatherForecasts",
                newName: "weather");
        }
    }
}
