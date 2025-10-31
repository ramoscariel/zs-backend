using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class BurnBarProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BarProducts",
                columns: new[] { "Id", "Name", "Qty", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("09df7b74-9cb1-4605-9fc7-5054b109c285"), "Red Wine Glass", 60, 6.25 },
                    { new Guid("16a4efed-7353-48ed-b046-2532bdc62c74"), "Craft IPA Beer", 110, 4.25 },
                    { new Guid("626e7d51-6573-4b46-b789-36756d9cffb5"), "Lager Beer", 120, 3.5 },
                    { new Guid("66274ceb-b662-4f3e-b413-7ebc64426af7"), "Whiskey Shot", 80, 5.75 },
                    { new Guid("8c758180-2375-4b27-bea4-d25e8a657608"), "Gin Tonic", 70, 6.0 },
                    { new Guid("afc8b253-4a00-4e0b-8993-5e74605ca49f"), "Tequila Shot", 100, 4.75 },
                    { new Guid("bd68b7a0-bce6-4159-818a-4bc084ae45fe"), "Margarita", 45, 7.5 },
                    { new Guid("ccde323d-2a39-4cd1-96d8-a634d3a87d33"), "Rum & Coke", 90, 5.25 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("09df7b74-9cb1-4605-9fc7-5054b109c285"));

            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("16a4efed-7353-48ed-b046-2532bdc62c74"));

            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("626e7d51-6573-4b46-b789-36756d9cffb5"));

            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("66274ceb-b662-4f3e-b413-7ebc64426af7"));

            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("8c758180-2375-4b27-bea4-d25e8a657608"));

            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("afc8b253-4a00-4e0b-8993-5e74605ca49f"));

            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("bd68b7a0-bce6-4159-818a-4bc084ae45fe"));

            migrationBuilder.DeleteData(
                table: "BarProducts",
                keyColumn: "Id",
                keyValue: new Guid("ccde323d-2a39-4cd1-96d8-a634d3a87d33"));
        }
    }
}
