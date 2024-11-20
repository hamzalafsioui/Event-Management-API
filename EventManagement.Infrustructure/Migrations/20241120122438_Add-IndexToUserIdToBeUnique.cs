using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventManagement.Infrustructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIndexToUserIdToBeUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Speakers_UserId",
                table: "Speakers");

            migrationBuilder.CreateIndex(
                name: "IX_Speaker_UserId",
                table: "Speakers",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Speaker_UserId",
                table: "Speakers");

            migrationBuilder.CreateIndex(
                name: "IX_Speakers_UserId",
                table: "Speakers",
                column: "UserId");
        }
    }
}
