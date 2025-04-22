using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CeilUfas.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLogoImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "LogoImagePath",
                table: "AppSettings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogoImagePath",
                table: "AppSettings");

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
    }
}
