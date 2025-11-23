using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class maketransactiontotransactionitem1to1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionItemId",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionItemId",
                table: "Transactions",
                column: "TransactionItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionItemId",
                table: "Transactions");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionItemId",
                table: "Transactions",
                column: "TransactionItemId");
        }
    }
}
