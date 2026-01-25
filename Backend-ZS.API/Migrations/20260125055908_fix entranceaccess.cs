using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class fixentranceaccess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                table: "EntranceAccessCards");

            migrationBuilder.DropColumn(
                name: "ExitTime",
                table: "EntranceAccessCards");

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
