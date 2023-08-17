using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PingPongTracker.migrations.AppMigrations
{
    /// <inheritdoc />
    public partial class UsernameAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Players");
        }
    }
}
