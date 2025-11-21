using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class mapaccesscardentrancetoaccesscard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccessCardId",
                table: "EntranceAccessCards",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EntranceAccessCards_AccessCardId",
                table: "EntranceAccessCards",
                column: "AccessCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntranceAccessCards_TransactionItems_AccessCardId",
                table: "EntranceAccessCards",
                column: "AccessCardId",
                principalTable: "TransactionItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntranceAccessCards_TransactionItems_AccessCardId",
                table: "EntranceAccessCards");

            migrationBuilder.DropIndex(
                name: "IX_EntranceAccessCards_AccessCardId",
                table: "EntranceAccessCards");

            migrationBuilder.DropColumn(
                name: "AccessCardId",
                table: "EntranceAccessCards");
        }
    }
}
