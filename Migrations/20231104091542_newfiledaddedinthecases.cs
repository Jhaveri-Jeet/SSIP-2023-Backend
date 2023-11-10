using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class newfiledaddedinthecases : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransferFromId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TransferToId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_TransferFromId",
                table: "Cases",
                column: "TransferFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_TransferToId",
                table: "Cases",
                column: "TransferToId");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Roles_TransferFromId",
                table: "Cases");

            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Roles_TransferToId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_TransferFromId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_TransferToId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "TransferFromId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "TransferToId",
                table: "Cases");
        }
    }
}
