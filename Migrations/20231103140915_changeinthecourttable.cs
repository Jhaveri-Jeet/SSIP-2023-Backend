using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class changeinthecourttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "Courts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courts_RoleId",
                table: "Courts",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Roles_RoleId",
                table: "Courts",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Roles_RoleId",
                table: "Courts");

            migrationBuilder.DropIndex(
                name: "IX_Courts_RoleId",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Courts");
        }
    }
}
