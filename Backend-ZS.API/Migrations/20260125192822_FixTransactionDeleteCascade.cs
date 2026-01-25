using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class FixTransactionDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_Transactions_TransactionId",
                table: "TransactionItem");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_Transactions_TransactionId",
                table: "TransactionItem",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_Transactions_TransactionId",
                table: "TransactionItem");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_Transactions_TransactionId",
                table: "TransactionItem",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
