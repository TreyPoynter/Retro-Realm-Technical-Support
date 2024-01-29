using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm.Migrations
{
    /// <inheritdoc />
    public partial class Incident : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Incidents",
                columns: table => new
                {
                    IncidentModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOpened = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateClosed = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustomerModelId = table.Column<int>(type: "int", nullable: false),
                    GameModelId = table.Column<int>(type: "int", nullable: false),
                    TechnicianModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidents", x => x.IncidentModelId);
                    table.ForeignKey(
                        name: "FK_Incidents_Customers_CustomerModelId",
                        column: x => x.CustomerModelId,
                        principalTable: "Customers",
                        principalColumn: "CustomerModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Games_GameModelId",
                        column: x => x.GameModelId,
                        principalTable: "Games",
                        principalColumn: "GameModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Incidents_Technicians_TechnicianModelId",
                        column: x => x.TechnicianModelId,
                        principalTable: "Technicians",
                        principalColumn: "TechnicianModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Incidents",
                columns: new[] { "IncidentModelId", "CustomerModelId", "DateClosed", "DateOpened", "Description", "GameModelId", "TechnicianModelId", "Title" },
                values: new object[] { 1, 1, null, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "When I insert cartridge the game flashes and crashes", 2, 3, "Error launching game" });

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_CustomerModelId",
                table: "Incidents",
                column: "CustomerModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_GameModelId",
                table: "Incidents",
                column: "GameModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Incidents_TechnicianModelId",
                table: "Incidents",
                column: "TechnicianModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Incidents");
        }
    }
}
