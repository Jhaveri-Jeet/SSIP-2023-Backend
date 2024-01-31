using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    /// <inheritdoc />
    public partial class tableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Remarks = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Status = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false),
                    DistrictsId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    CourtsId = table.Column<int>(type: "int", nullable: true),
                    RequestedCourtId = table.Column<int>(type: "int", nullable: false),
                    TCourtId = table.Column<int>(type: "int", nullable: false),
                    TransferCourtToIdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessRequest_Cases_CaseId",
                        column: x => x.CaseId,
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessRequest_Courts_CourtsId",
                        column: x => x.CourtsId,
                        principalTable: "Courts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRequest_Courts_RequestedCourtId",
                        column: x => x.RequestedCourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessRequest_Courts_TransferCourtToIdId",
                        column: x => x.TransferCourtToIdId,
                        principalTable: "Courts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRequest_Districts_DistrictsId",
                        column: x => x.DistrictsId,
                        principalTable: "Districts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AccessRequest_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessRequest_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_CaseId",
                table: "AccessRequest",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_CourtsId",
                table: "AccessRequest",
                column: "CourtsId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_DistrictsId",
                table: "AccessRequest",
                column: "DistrictsId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_RequestedCourtId",
                table: "AccessRequest",
                column: "RequestedCourtId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_RoleId",
                table: "AccessRequest",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_StateId",
                table: "AccessRequest",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessRequest_TransferCourtToIdId",
                table: "AccessRequest",
                column: "TransferCourtToIdId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessRequest");
        }
    }
}
