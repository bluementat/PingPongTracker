using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingPongTracker.Migrations.AppMigrations
{
    /// <inheritdoc />
    public partial class ExpandGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Team1Name",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Team2Name",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Team1Name",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "Team2Name",
                table: "Games");
        }
    }
}
