using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RetroRealm.Migrations
{
    /// <inheritdoc />
    public partial class Customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CountryModel",
                columns: table => new
                {
                    CountryModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryModel", x => x.CountryModelId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerModel",
                columns: table => new
                {
                    CustomerModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerModel", x => x.CustomerModelId);
                    table.ForeignKey(
                        name: "FK_CustomerModel_CountryModel_CountryModelId",
                        column: x => x.CountryModelId,
                        principalTable: "CountryModel",
                        principalColumn: "CountryModelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CountryModel",
                columns: new[] { "CountryModelId", "Country" },
                values: new object[,]
                {
                    { 1, "United States" },
                    { 2, "Canada" },
                    { 3, "United Kingdom" },
                    { 4, "Germany" },
                    { 5, "France" },
                    { 6, "Australia" },
                    { 7, "Japan" },
                    { 8, "Brazil" },
                    { 9, "India" },
                    { 10, "South Africa" }
                });

            migrationBuilder.InsertData(
                table: "CustomerModel",
                columns: new[] { "CustomerModelId", "Address", "City", "CountryModelId", "Email", "Firstname", "Lastname", "Phone", "PostalCode", "State" },
                values: new object[] { 1, "989 Beach Way", "San Fransisco", 1, "ania@yahoo.com", "Ania", "Irvin", "314-890-7889", "94110", "CA" });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerModel_CountryModelId",
                table: "CustomerModel",
                column: "CountryModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerModel");

            migrationBuilder.DropTable(
                name: "CountryModel");
        }
    }
}
