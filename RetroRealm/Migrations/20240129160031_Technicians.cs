using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetroRealm.Migrations
{
    /// <inheritdoc />
    public partial class Technicians : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technicians",
                columns: table => new
                {
                    TechnicianModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.TechnicianModelId);
                });

            migrationBuilder.InsertData(
                table: "Technicians",
                columns: new[] { "TechnicianModelId", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "alison@retrorealmsoftware.com", "Alison Diaz", "800-555-0449" },
                    { 2, "tonyc@retrorealmsoftware.com", "Tony Chef", "314-123-4567" },
                    { 3, "poynter@retrorealmsoftware.com", "Trey Poynter", "573-789-1234" },
                    { 4, "johnd@retrorealmsoftware.com", "John Doe", "111-222-3333" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Technicians");
        }
    }
}
