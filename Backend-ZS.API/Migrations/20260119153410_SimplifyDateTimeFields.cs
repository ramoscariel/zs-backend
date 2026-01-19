using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyDateTimeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntranceDate",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "EntranceEntryTime",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "EntranceExitTime",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "ParkingDate",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "ParkingEntryTime",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "ParkingExitTime",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "EntranceDate",
                table: "EntranceAccessCards");

            migrationBuilder.DropColumn(
                name: "EntranceEntryTime",
                table: "EntranceAccessCards");

            migrationBuilder.DropColumn(
                name: "EntranceExitTime",
                table: "EntranceAccessCards");

            migrationBuilder.AddColumn<DateTime>(
                name: "EntryTime",
                table: "TransactionItem",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExitTime",
                table: "TransactionItem",
                type: "datetime2",
                nullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "ExitTime",
                table: "TransactionItem");

            migrationBuilder.DropColumn(
                name: "EntryTime",
                table: "EntranceAccessCards");

            migrationBuilder.DropColumn(
                name: "ExitTime",
                table: "EntranceAccessCards");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntranceDate",
                table: "TransactionItem",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntranceEntryTime",
                table: "TransactionItem",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntranceExitTime",
                table: "TransactionItem",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "ParkingDate",
                table: "TransactionItem",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ParkingEntryTime",
                table: "TransactionItem",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "ParkingExitTime",
                table: "TransactionItem",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntranceDate",
                table: "EntranceAccessCards",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntranceEntryTime",
                table: "EntranceAccessCards",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntranceExitTime",
                table: "EntranceAccessCards",
                type: "time",
                nullable: true);
        }
    }
}
