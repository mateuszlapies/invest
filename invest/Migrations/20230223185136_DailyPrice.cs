using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace invest.Migrations
{
    /// <inheritdoc />
    public partial class DailyPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Items",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Daily",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    MedianPrice = table.Column<double>(type: "double precision", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Daily", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Daily_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId");
                });

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
                columns: new[] { "BuyAmount", "Currency", "Order" },
                values: new object[] { 40, 5, 0 });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "BuyAmount", "BuyPrice", "Currency", "Hash", "Name", "Order", "Url" },
                values: new object[] { 2, 3, 0.0, 5, "Operation%20Hydra%20Case", "Operation Hydra Case", 1, null });

            migrationBuilder.CreateIndex(
                name: "IX_Daily_ItemId",
                table: "Daily",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Daily");

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "ItemId",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Items");

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
                columns: new[] { "BuyAmount", "Currency" },
                values: new object[] { 30, 0 });
        }
    }
}
