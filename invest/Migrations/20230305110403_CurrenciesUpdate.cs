using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invest.Migrations
{
    /// <inheritdoc />
    public partial class CurrenciesUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "Currency",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2,
                column: "Currency",
                value: 6);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 1,
                column: "Expires",
                value: new DateTime(2023, 3, 1, 18, 51, 36, 877, DateTimeKind.Utc).AddTicks(907));

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 2,
                column: "Expires",
                value: new DateTime(2023, 3, 1, 18, 51, 36, 877, DateTimeKind.Utc).AddTicks(959));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "Currency",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2,
                column: "Currency",
                value: 5);
        }
    }
}
