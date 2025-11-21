using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class AddEntranceTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExitTime",
                table: "TransactionItems",
                newName: "ParkingExitTime");

            migrationBuilder.RenameColumn(
                name: "EntryTime",
                table: "TransactionItems",
                newName: "ParkingEntryTime");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "TransactionItems",
                newName: "ParkingDate");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EntranceDate",
                table: "TransactionItems",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntranceEntryTime",
                table: "TransactionItems",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "EntranceExitTime",
                table: "TransactionItems",
                type: "time",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberAdults",
                table: "TransactionItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberChildren",
                table: "TransactionItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberDisabled",
                table: "TransactionItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberPersons",
                table: "TransactionItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberSeniors",
                table: "TransactionItems",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntranceDate",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "EntranceEntryTime",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "EntranceExitTime",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "NumberAdults",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "NumberChildren",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "NumberDisabled",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "NumberPersons",
                table: "TransactionItems");

            migrationBuilder.DropColumn(
                name: "NumberSeniors",
                table: "TransactionItems");

            migrationBuilder.RenameColumn(
                name: "ParkingExitTime",
                table: "TransactionItems",
                newName: "ExitTime");

            migrationBuilder.RenameColumn(
                name: "ParkingEntryTime",
                table: "TransactionItems",
                newName: "EntryTime");

            migrationBuilder.RenameColumn(
                name: "ParkingDate",
                table: "TransactionItems",
                newName: "Date");
        }
    }
}
