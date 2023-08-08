using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingPongTracker.migrations.AppMigrations
{
    /// <inheritdoc />
    public partial class ExpandPlayer01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Eligible",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Eligible",
                table: "Players");
        }
    }
}
