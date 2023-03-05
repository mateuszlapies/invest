using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invest.Migrations
{
    /// <inheritdoc />
    public partial class DailyTimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Timestamp",
                table: "Daily",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 1,
                column: "Expires",
                value: new DateTime(2023, 3, 11, 11, 52, 38, 182, DateTimeKind.Utc).AddTicks(357));

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 2,
                column: "Expires",
                value: new DateTime(2023, 3, 11, 11, 52, 38, 182, DateTimeKind.Utc).AddTicks(461));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Daily");

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 1,
                column: "Expires",
                value: new DateTime(2023, 3, 11, 11, 4, 3, 186, DateTimeKind.Utc).AddTicks(5002));

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 2,
                column: "Expires",
                value: new DateTime(2023, 3, 11, 11, 4, 3, 186, DateTimeKind.Utc).AddTicks(5116));
        }
    }
}
