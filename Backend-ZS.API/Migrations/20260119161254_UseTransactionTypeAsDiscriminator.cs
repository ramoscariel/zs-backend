using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class UseTransactionTypeAsDiscriminator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // First sync data from Discriminator to TransactionType
            migrationBuilder.Sql(@"
                UPDATE TransactionItem
                SET TransactionType = Discriminator
                WHERE TransactionType IS NULL
                   OR TransactionType = ''
                   OR TransactionType <> Discriminator
            ");

            // Change TransactionType column type
            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "TransactionItem",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Drop redundant Discriminator column
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "TransactionItem");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Add back the Discriminator column
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "TransactionItem",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            // Copy data from TransactionType to Discriminator
            migrationBuilder.Sql("UPDATE TransactionItem SET Discriminator = TransactionType");

            // Revert TransactionType column type
            migrationBuilder.AlterColumn<string>(
                name: "TransactionType",
                table: "TransactionItem",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(21)",
                oldMaxLength: 21);
        }
    }
}
