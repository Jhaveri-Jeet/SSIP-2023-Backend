using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class casestablechanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Roles_TransferFromId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Roles_TransferToId",
                table: "Cases");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Courts_TransferFromId",
                table: "Cases",
                column: "TransferFromId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Courts_TransferToId",
                table: "Cases",
                column: "TransferToId",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Courts_TransferFromId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Courts_TransferToId",
                table: "Cases");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Roles_TransferFromId",
                table: "Cases",
                column: "TransferFromId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Roles_TransferToId",
                table: "Cases",
                column: "TransferToId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
