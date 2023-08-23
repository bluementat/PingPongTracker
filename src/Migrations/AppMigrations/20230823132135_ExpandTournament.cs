using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingPongTracker.Migrations.AppMigrations
{
    /// <inheritdoc />
    public partial class ExpandTournament : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsComplete",
                table: "Tournaments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "TournamentEndDate",
                table: "Tournaments",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsComplete",
                table: "Tournaments");

            migrationBuilder.DropColumn(
                name: "TournamentEndDate",
                table: "Tournaments");
        }
    }
}
