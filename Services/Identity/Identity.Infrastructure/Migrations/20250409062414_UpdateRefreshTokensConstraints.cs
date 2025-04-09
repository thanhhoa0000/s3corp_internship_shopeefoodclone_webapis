using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRefreshTokensConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("28d9bf93-2825-4a40-ad84-f8a964e5eaae"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("45e3a5ab-56c1-411d-8469-56c9bd9ea00d"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("dc20df28-4094-427a-a959-8c3850e7608b"));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("913ae6d5-7bef-43e8-8db3-8a162a71518f"), "00000000-0000-0000-0000-000000000000", "Customer role", "Customer", "CUSTOMER" },
                    { new Guid("9e75b1d1-58a1-4a9a-ada9-39a545d6e5e5"), "00000000-0000-0000-0000-000000000000", "Vendor role", "Vendor", "VENDOR" },
                    { new Guid("ceaf2fd0-ab94-43e4-9a50-d8f37b7377c9"), "00000000-0000-0000-0000-000000000000", "Administrator role", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens");

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("913ae6d5-7bef-43e8-8db3-8a162a71518f"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("9e75b1d1-58a1-4a9a-ada9-39a545d6e5e5"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("ceaf2fd0-ab94-43e4-9a50-d8f37b7377c9"));

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("28d9bf93-2825-4a40-ad84-f8a964e5eaae"), "00000000-0000-0000-0000-000000000000", "Vendor role", "Vendor", "VENDOR" },
                    { new Guid("45e3a5ab-56c1-411d-8469-56c9bd9ea00d"), "00000000-0000-0000-0000-000000000000", "Customer role", "Customer", "CUSTOMER" },
                    { new Guid("dc20df28-4094-427a-a959-8c3850e7608b"), "00000000-0000-0000-0000-000000000000", "Administrator role", "Admin", "ADMIN" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_AppUserId",
                table: "RefreshTokens",
                column: "AppUserId");
        }
    }
}
