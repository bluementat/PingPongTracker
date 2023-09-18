using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingPongTracker.Migrations.AppMigrations
{
    /// <inheritdoc />
    public partial class TourneyGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "GameId");

            migrationBuilder.CreateTable(
                name: "TourneyGames",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team1Player1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team1Player2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team2Player1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team2Player2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team1Score = table.Column<int>(type: "int", nullable: false),
                    Team2Score = table.Column<int>(type: "int", nullable: false),
                    MatchupDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Player1WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player2WinnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourneyGames", x => x.GameId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TourneyGames");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "GameId");
        }
    }
}
