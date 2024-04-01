using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetroRealm.Migrations
{
    /// <inheritdoc />
    public partial class FixedJointTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripActivities");

            migrationBuilder.CreateTable(
                name: "CustomerGames",
                columns: table => new
                {
                    CustomerModelId = table.Column<int>(type: "int", nullable: false),
                    GameModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerGames", x => new { x.CustomerModelId, x.GameModelId });
                    table.ForeignKey(
                        name: "FK_CustomerGames_Customers_CustomerModelId",
                        column: x => x.CustomerModelId,
                        principalTable: "Customers",
                        principalColumn: "CustomerModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerGames_Games_GameModelId",
                        column: x => x.GameModelId,
                        principalTable: "Games",
                        principalColumn: "GameModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CustomerGames",
                columns: new[] { "CustomerModelId", "GameModelId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerGames_GameModelId",
                table: "CustomerGames",
                column: "GameModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerGames");

            migrationBuilder.CreateTable(
                name: "TripActivities",
                columns: table => new
                {
                    CustomerModelId = table.Column<int>(type: "int", nullable: false),
                    GameModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripActivities", x => new { x.CustomerModelId, x.GameModelId });
                    table.ForeignKey(
                        name: "FK_TripActivities_Customers_CustomerModelId",
                        column: x => x.CustomerModelId,
                        principalTable: "Customers",
                        principalColumn: "CustomerModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripActivities_Games_GameModelId",
                        column: x => x.GameModelId,
                        principalTable: "Games",
                        principalColumn: "GameModelId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "TripActivities",
                columns: new[] { "CustomerModelId", "GameModelId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripActivities_GameModelId",
                table: "TripActivities",
                column: "GameModelId");
        }
    }
}
