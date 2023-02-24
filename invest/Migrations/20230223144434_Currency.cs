using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace invest.Migrations
{
    /// <inheritdoc />
    public partial class Currency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Currency",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 1,
                column: "Expires",
                value: new DateTime(2023, 3, 1, 14, 44, 34, 171, DateTimeKind.Utc));

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 2,
                column: "Expires",
                value: new DateTime(2023, 3, 1, 14, 44, 34, 171, DateTimeKind.Utc).AddTicks(54));

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 1,
                column: "Currency",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Items");

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 1,
                column: "Expires",
                value: new DateTime(2023, 2, 27, 18, 11, 46, 906, DateTimeKind.Utc).AddTicks(800));

            migrationBuilder.UpdateData(
                table: "Cookies",
                keyColumn: "CookieId",
                keyValue: 2,
                column: "Expires",
                value: new DateTime(2023, 2, 27, 18, 11, 46, 906, DateTimeKind.Utc).AddTicks(851));
        }
    }
}
