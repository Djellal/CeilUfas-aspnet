using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilUfas.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCurrentSessiontoAppSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentSessionId",
                table: "AppSettings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppSettings_CurrentSessionId",
                table: "AppSettings",
                column: "CurrentSessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppSettings_Sessions_CurrentSessionId",
                table: "AppSettings",
                column: "CurrentSessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppSettings_Sessions_CurrentSessionId",
                table: "AppSettings");

            migrationBuilder.DropIndex(
                name: "IX_AppSettings_CurrentSessionId",
                table: "AppSettings");

            migrationBuilder.DropColumn(
                name: "CurrentSessionId",
                table: "AppSettings");
        }
    }
}
