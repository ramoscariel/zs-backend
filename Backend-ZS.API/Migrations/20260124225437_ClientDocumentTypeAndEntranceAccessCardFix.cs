using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class ClientDocumentTypeAndEntranceAccessCardFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "EntranceAccessCards");

            migrationBuilder.DropColumn(
                name: "ExitTime",
                table: "EntranceAccessCards");

            migrationBuilder.RenameColumn(
                name: "NationalId",
                table: "Clients",
                newName: "DocumentNumber");

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "Clients",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("1f8a42f5-9e9a-4c1b-bc5a-8d4129fd78c2"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("3a28b14c-4f9d-4d4c-9021-1894a8b6a2d1"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("b02cfb22-4e26-4dd3-8e62-43d29cb86e1e"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("b68c1f84-4a8c-4ee3-8a6a-4b35a27ad331"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("c8b5c2df-4b91-4239-b134-88a3d52f91d8"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("d5127f6c-6b5b-4b4a-a9e1-2b8d4f508e45"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("e6ab1df7-b60b-4db9-96f7-4f6cb5f00219"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("e71b9b12-84c3-4c6b-b6e5-5f889b5cf2b7"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                column: "DocumentType",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: new Guid("f97b5672-00b9-48c9-85b4-3b897e8af8bb"),
                column: "DocumentType",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "Clients");

            migrationBuilder.RenameColumn(
                name: "DocumentNumber",
                table: "Clients",
                newName: "NationalId");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryTime",
                table: "EntranceAccessCards",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ExitTime",
                table: "EntranceAccessCards",
                type: "datetime2",
                nullable: true);
        }
    }
}
