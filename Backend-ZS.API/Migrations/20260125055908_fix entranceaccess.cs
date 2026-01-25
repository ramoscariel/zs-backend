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
            // Drop previous separate date/time columns only if they exist (avoid errors when already removed)
            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'EntranceDate' AND object_id = OBJECT_ID(N'[EntranceAccessCards]'))
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EntranceAccessCards]') AND [c].[name] = N'EntranceDate');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [EntranceAccessCards] DROP CONSTRAINT [' + @var + ']');
    ALTER TABLE [EntranceAccessCards] DROP COLUMN [EntranceDate];
END
");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'EntranceEntryTime' AND object_id = OBJECT_ID(N'[EntranceAccessCards]'))
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EntranceAccessCards]') AND [c].[name] = N'EntranceEntryTime');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [EntranceAccessCards] DROP CONSTRAINT [' + @var + ']');
    ALTER TABLE [EntranceAccessCards] DROP COLUMN [EntranceEntryTime];
END
");

            migrationBuilder.Sql(@"
IF EXISTS (SELECT 1 FROM sys.columns WHERE Name = N'EntranceExitTime' AND object_id = OBJECT_ID(N'[EntranceAccessCards]'))
BEGIN
    DECLARE @var sysname;
    SELECT @var = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[EntranceAccessCards]') AND [c].[name] = N'EntranceExitTime');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [EntranceAccessCards] DROP CONSTRAINT [' + @var + ']');
    ALTER TABLE [EntranceAccessCards] DROP COLUMN [EntranceExitTime];
END
");

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
