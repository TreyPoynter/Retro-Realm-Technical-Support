using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm.Migrations
{
    /// <inheritdoc />
    public partial class NotAssigned : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Technicians",
                columns: new[] { "TechnicianModelId", "Email", "Name", "Phone" },
                values: new object[] { -1, "", "Not Assigned", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Technicians",
                keyColumn: "TechnicianModelId",
                keyValue: -1);
        }
    }
}
