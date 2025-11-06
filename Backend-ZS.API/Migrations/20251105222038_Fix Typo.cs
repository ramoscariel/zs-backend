using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class FixTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarOrderDetails_TransactionItem_BarOrderId",
                table: "BarOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionItem_TransactionItemId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItem",
                table: "TransactionItem");

            migrationBuilder.RenameTable(
                name: "TransactionItem",
                newName: "TransactionItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionItems",
                table: "TransactionItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarOrderDetails_TransactionItems_BarOrderId",
                table: "BarOrderDetails",
                column: "BarOrderId",
                principalTable: "TransactionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionItems_TransactionItemId",
                table: "Transactions",
                column: "TransactionItemId",
                principalTable: "TransactionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarOrderDetails_TransactionItems_BarOrderId",
                table: "BarOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionItems_TransactionItemId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItems",
                table: "TransactionItems");

            migrationBuilder.RenameTable(
                name: "TransactionItems",
                newName: "TransactionItem");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionItem",
                table: "TransactionItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BarOrderDetails_TransactionItem_BarOrderId",
                table: "BarOrderDetails",
                column: "BarOrderId",
                principalTable: "TransactionItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionItem_TransactionItemId",
                table: "Transactions",
                column: "TransactionItemId",
                principalTable: "TransactionItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
