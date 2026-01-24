using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_ZS.API.Migrations
{
    /// <inheritdoc />
    public partial class SyncModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "EntranceAccessCards",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
