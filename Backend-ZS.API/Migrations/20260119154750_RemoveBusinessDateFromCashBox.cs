using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBusinessDateFromCashBox : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CashBoxes_BusinessDate",
                table: "CashBoxes");

            migrationBuilder.DropColumn(
                name: "BusinessDate",
                table: "CashBoxes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "BusinessDate",
                table: "CashBoxes",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_CashBoxes_BusinessDate",
                table: "CashBoxes",
                column: "BusinessDate",
                unique: true);
        }
    }
}
