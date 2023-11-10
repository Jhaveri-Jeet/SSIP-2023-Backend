using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class changeinuserstableee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Courts_CourtsId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Courts_CourtsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CourtsId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CourtsId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "CourtsId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CourtsId",
                table: "Cases");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CourtId",
                table: "Users",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtId",
                table: "Cases",
                column: "CourtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Courts_CourtId",
                table: "Cases",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Courts_CourtId",
                table: "Users",
                column: "CourtId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Courts_CourtId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Courts_CourtId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CourtId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Cases_CourtId",
                table: "Cases");

            migrationBuilder.AddColumn<int>(
                name: "CourtsId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CourtsId",
                table: "Cases",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CourtsId",
                table: "Users",
                column: "CourtsId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtsId",
                table: "Cases",
                column: "CourtsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Courts_CourtsId",
                table: "Cases",
                column: "CourtsId",
                principalTable: "Courts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Courts_CourtsId",
                table: "Users",
                column: "CourtsId",
                principalTable: "Courts",
                principalColumn: "Id");
        }
    }
}
