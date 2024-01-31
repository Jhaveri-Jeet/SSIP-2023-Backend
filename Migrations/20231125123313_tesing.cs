using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class tesing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TCourtId",
                table: "AccessRequest",
                newName: "TransferReqestCourtId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransferReqestCourtId",
                table: "AccessRequest",
                newName: "TCourtId");
        }
    }
}
