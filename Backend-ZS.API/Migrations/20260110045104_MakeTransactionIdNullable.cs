using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class MakeTransactionIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "TransactionItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "TransactionId",
                table: "TransactionItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
