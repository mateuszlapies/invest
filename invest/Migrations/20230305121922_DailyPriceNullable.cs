using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invest.Migrations
{
    /// <inheritdoc />
    public partial class DailyPriceNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Daily",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision");

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 1,
                column: "Expires",
                value: new DateTime(2023, 3, 11, 12, 19, 21, 932, DateTimeKind.Utc).AddTicks(5269));

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 2,
                column: "Expires",
                value: new DateTime(2023, 3, 11, 12, 19, 21, 932, DateTimeKind.Utc).AddTicks(5368));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Daily",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

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
    }
}
