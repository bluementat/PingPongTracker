using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingPongTracker.Migrations.AppMigrations
{
    /// <inheritdoc />
    public partial class RemoveTourneyChangeGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Tournaments_TournamentId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Tournaments_TournamentId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropIndex(
                name: "IX_Players_TournamentId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Games_TournamentId",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "TournamentId",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "TournamentId",
                table: "Games",
                newName: "SeasonId");

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    TeamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Player1Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player1UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Player2Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Player2UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.TeamID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.RenameColumn(
                name: "SeasonId",
                table: "Games",
                newName: "TournamentId");

            migrationBuilder.AddColumn<Guid>(
                name: "TournamentId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    TournamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeasonId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TournamentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TournamentEndDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.TournamentId);
                    table.ForeignKey(
                        name: "FK_Tournaments_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "SeasonId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_TournamentId",
                table: "Players",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_TournamentId",
                table: "Games",
                column: "TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_SeasonId",
                table: "Tournaments",
                column: "SeasonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Tournaments_TournamentId",
                table: "Games",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Tournaments_TournamentId",
                table: "Players",
                column: "TournamentId",
                principalTable: "Tournaments",
                principalColumn: "TournamentId");
        }
    }
}
