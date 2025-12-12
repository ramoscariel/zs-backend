using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangerelationshipbtweenTransactionandTransactionItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Clients_ClientId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Payments_PaymentId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_TransactionItems_TransactionItemId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PaymentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_TransactionItemId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionItemId",
                table: "Transactions");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "TransactionItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionId",
                table: "TransactionItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentId",
                table: "Transactions",
                column: "PaymentId",
                unique: true,
                filter: "[PaymentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Clients_ClientId",
                table: "Transactions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Payments_PaymentId",
                table: "Transactions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Clients_ClientId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Payments_PaymentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PaymentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "TransactionItems");

            migrationBuilder.AddColumn<Guid>(
                name: "TransactionItemId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PaymentId",
                table: "Transactions",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionItemId",
                table: "Transactions",
                column: "TransactionItemId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Clients_ClientId",
                table: "Transactions",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Payments_PaymentId",
                table: "Transactions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_TransactionItems_TransactionItemId",
                table: "Transactions",
                column: "TransactionItemId",
                principalTable: "TransactionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
