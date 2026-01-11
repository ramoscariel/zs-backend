using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddCashBoxAndLinkTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarOrderDetails_TransactionItems_BarOrderId",
                table: "BarOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EntranceAccessCards_TransactionItems_AccessCardId",
                table: "EntranceAccessCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItems",
                table: "TransactionItems");

            migrationBuilder.RenameTable(
                name: "TransactionItems",
                newName: "TransactionItem");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Transactions",
                newName: "OpenedAt");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionItems_TransactionId",
                table: "TransactionItem",
                newName: "IX_TransactionItem_TransactionId");

            migrationBuilder.AddColumn<Guid>(
                name: "CashBoxId",
                table: "Transactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ClosedAt",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TransactionItem",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TransactionItem",
                table: "TransactionItem",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CashBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BusinessDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClosedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    OpeningBalance = table.Column<double>(type: "float", nullable: false),
                    ClosingBalance = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashBoxes", x => x.Id);
                });

            // ✅ FIX CRÍTICO:
            // Existe Transactions.CashBoxId NOT NULL con default 0000..., entonces creamos una CashBox legacy
            // con ese mismo Id para que el FK no falle al aplicar la migración.
            var legacyCashBoxId = new Guid("00000000-0000-0000-0000-000000000000");

            migrationBuilder.InsertData(
                table: "CashBoxes",
                columns: new[] { "Id", "BusinessDate", "Status", "OpenedAt", "ClosedAt", "OpeningBalance", "ClosingBalance" },
                values: new object[]
                {
                    legacyCashBoxId,
                    new DateOnly(2000, 1, 1),
                    "Closed",
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    0.0,
                    0.0
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CashBoxId",
                table: "Transactions",
                column: "CashBoxId");

            migrationBuilder.CreateIndex(
                name: "IX_CashBoxes_BusinessDate",
                table: "CashBoxes",
                column: "BusinessDate",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BarOrderDetails_TransactionItem_BarOrderId",
                table: "BarOrderDetails",
                column: "BarOrderId",
                principalTable: "TransactionItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceAccessCards_TransactionItem_AccessCardId",
                table: "EntranceAccessCards",
                column: "AccessCardId",
                principalTable: "TransactionItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItem_Transactions_TransactionId",
                table: "TransactionItem",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CashBoxes_CashBoxId",
                table: "Transactions",
                column: "CashBoxId",
                principalTable: "CashBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BarOrderDetails_TransactionItem_BarOrderId",
                table: "BarOrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EntranceAccessCards_TransactionItem_AccessCardId",
                table: "EntranceAccessCards");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionItem_Transactions_TransactionId",
                table: "TransactionItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CashBoxes_CashBoxId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "CashBoxes");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CashBoxId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TransactionItem",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "CashBoxId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ClosedAt",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TransactionItem");

            migrationBuilder.RenameTable(
                name: "TransactionItem",
                newName: "TransactionItems");

            migrationBuilder.RenameColumn(
                name: "OpenedAt",
                table: "Transactions",
                newName: "CreatedAt");

            migrationBuilder.RenameIndex(
                name: "IX_TransactionItem_TransactionId",
                table: "TransactionItems",
                newName: "IX_TransactionItems_TransactionId");

            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "TransactionItems",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
                name: "FK_EntranceAccessCards_TransactionItems_AccessCardId",
                table: "EntranceAccessCards",
                column: "AccessCardId",
                principalTable: "TransactionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionItems_Transactions_TransactionId",
                table: "TransactionItems",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}
