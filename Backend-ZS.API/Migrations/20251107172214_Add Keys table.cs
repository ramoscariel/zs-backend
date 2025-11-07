using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddKeystable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KeyCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAssignedTo = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Available = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keys_Clients_LastAssignedTo",
                        column: x => x.LastAssignedTo,
                        principalTable: "Clients",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Keys_LastAssignedTo",
                table: "Keys",
                column: "LastAssignedTo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keys");
        }
    }
}
