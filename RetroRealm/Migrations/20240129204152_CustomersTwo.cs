using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RetroRealm.Migrations
{
    /// <inheritdoc />
    public partial class CustomersTwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerModel_CountryModel_CountryModelId",
                table: "CustomerModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CustomerModel",
                table: "CustomerModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CountryModel",
                table: "CountryModel");

            migrationBuilder.RenameTable(
                name: "CustomerModel",
                newName: "Customers");

            migrationBuilder.RenameTable(
                name: "CountryModel",
                newName: "Countries");

            migrationBuilder.RenameIndex(
                name: "IX_CustomerModel_CountryModelId",
                table: "Customers",
                newName: "IX_Customers_CountryModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Customers",
                table: "Customers",
                column: "CustomerModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Countries",
                table: "Countries",
                column: "CountryModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Countries_CountryModelId",
                table: "Customers",
                column: "CountryModelId",
                principalTable: "Countries",
                principalColumn: "CountryModelId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Countries_CountryModelId",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Customers",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Countries",
                table: "Countries");

            migrationBuilder.RenameTable(
                name: "Customers",
                newName: "CustomerModel");

            migrationBuilder.RenameTable(
                name: "Countries",
                newName: "CountryModel");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_CountryModelId",
                table: "CustomerModel",
                newName: "IX_CustomerModel_CountryModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CustomerModel",
                table: "CustomerModel",
                column: "CustomerModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CountryModel",
                table: "CountryModel",
                column: "CountryModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerModel_CountryModel_CountryModelId",
                table: "CustomerModel",
                column: "CountryModelId",
                principalTable: "CountryModel",
                principalColumn: "CountryModelId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
