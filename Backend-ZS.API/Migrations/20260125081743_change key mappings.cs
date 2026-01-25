using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class changekeymappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Clients_LastAssignedTo",
                table: "Keys");

            migrationBuilder.RenameColumn(
                name: "LastAssignedTo",
                table: "Keys",
                newName: "TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Keys_LastAssignedTo",
                table: "Keys",
                newName: "IX_Keys_TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Transactions_TransactionId",
                table: "Keys",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keys_Transactions_TransactionId",
                table: "Keys");

            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Keys",
                newName: "LastAssignedTo");

            migrationBuilder.RenameIndex(
                name: "IX_Keys_TransactionId",
                table: "Keys",
                newName: "IX_Keys_LastAssignedTo");

            migrationBuilder.AddForeignKey(
                name: "FK_Keys_Clients_LastAssignedTo",
                table: "Keys",
                column: "LastAssignedTo",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
