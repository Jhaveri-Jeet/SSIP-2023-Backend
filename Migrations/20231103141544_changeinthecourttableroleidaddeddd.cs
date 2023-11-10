using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class changeinthecourttableroleidaddeddd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courts_Roles_RolesId",
                table: "Courts");

            migrationBuilder.DropIndex(
                name: "IX_Courts_RolesId",
                table: "Courts");

            migrationBuilder.DropColumn(
                name: "RolesId",
                table: "Courts");

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

            migrationBuilder.AddColumn<int>(
                name: "RolesId",
                table: "Courts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courts_RolesId",
                table: "Courts",
                column: "RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courts_Roles_RolesId",
                table: "Courts",
                column: "RolesId",
                principalTable: "Roles",
                principalColumn: "Id");
        }
    }
}
