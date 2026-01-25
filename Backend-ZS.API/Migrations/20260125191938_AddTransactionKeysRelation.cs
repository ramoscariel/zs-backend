using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionKeysRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Transactions_TransactionId",
                table: "Keys");

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Transactions_TransactionId",
                table: "Keys",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Transactions_TransactionId",
                table: "Keys");

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Transactions_TransactionId",
                table: "Keys",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
