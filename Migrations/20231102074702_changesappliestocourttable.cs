using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class changesappliestocourttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Districts_districtId",
                table: "Courts");

            migrationBuilder.DropForeignKey(
                name: "FK_Courts_States_stateId",
                table: "Courts");

            migrationBuilder.RenameColumn(
                name: "stateId",
                table: "Courts",
                newName: "StateId");

            migrationBuilder.RenameColumn(
                name: "districtId",
                table: "Courts",
                newName: "DistrictId");

            migrationBuilder.RenameIndex(
                name: "IX_Courts_stateId",
                table: "Courts",
                newName: "IX_Courts_StateId");

            migrationBuilder.RenameIndex(
                name: "IX_Courts_districtId",
                table: "Courts",
                newName: "IX_Courts_DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Districts_DistrictId",
                table: "Courts",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_States_StateId",
                table: "Courts",
                column: "StateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Districts_DistrictId",
                table: "Courts");

            migrationBuilder.DropForeignKey(
                name: "FK_Courts_States_StateId",
                table: "Courts");

            migrationBuilder.RenameColumn(
                name: "StateId",
                table: "Courts",
                newName: "stateId");

            migrationBuilder.RenameColumn(
                name: "DistrictId",
                table: "Courts",
                newName: "districtId");

            migrationBuilder.RenameIndex(
                name: "IX_Courts_StateId",
                table: "Courts",
                newName: "IX_Courts_stateId");

            migrationBuilder.RenameIndex(
                name: "IX_Courts_DistrictId",
                table: "Courts",
                newName: "IX_Courts_districtId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Districts_districtId",
                table: "Courts",
                column: "districtId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_States_stateId",
                table: "Courts",
                column: "stateId",
                principalTable: "States",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
