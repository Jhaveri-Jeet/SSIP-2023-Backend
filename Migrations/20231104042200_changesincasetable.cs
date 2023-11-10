using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class changesincasetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_RoleId",
                table: "Cases",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Roles_RoleId",
                table: "Cases",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Roles_RoleId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_RoleId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Cases");
        }
    }
}
