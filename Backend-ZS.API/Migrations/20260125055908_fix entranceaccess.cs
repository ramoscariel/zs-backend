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
            // Columns were already dropped and added by SimplifyDateTimeFields migration
            // This migration is now a no-op
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No-op - see Up() comment
        }
    }
}
