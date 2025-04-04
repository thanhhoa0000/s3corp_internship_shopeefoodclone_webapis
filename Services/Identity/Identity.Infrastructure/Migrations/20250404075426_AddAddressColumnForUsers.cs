using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ShopeeFoodClone.WebApi.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAddressColumnForUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("1514474e-63e4-47bc-b3b8-fd1c174b9e5f"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("a7b5d50e-f703-4790-ae4c-234fc6fe6df1"));

            migrationBuilder.DeleteData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("f6fe96d6-874b-4c9e-a80e-b35d430029da"));

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "AppUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("28d9bf93-2825-4a40-ad84-f8a964e5eaae"), "00000000-0000-0000-0000-000000000000", "Vendor role", "Vendor", "VENDOR" },
                    { new Guid("45e3a5ab-56c1-411d-8469-56c9bd9ea00d"), "00000000-0000-0000-0000-000000000000", "Customer role", "Customer", "CUSTOMER" },
                    { new Guid("dc20df28-4094-427a-a959-8c3850e7608b"), "00000000-0000-0000-0000-000000000000", "Administrator role", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.DropColumn(
                name: "Address",
                table: "AppUsers");

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("1514474e-63e4-47bc-b3b8-fd1c174b9e5f"), "00000000-0000-0000-0000-000000000000", "Customer role", "Customer", "CUSTOMER" },
                    { new Guid("a7b5d50e-f703-4790-ae4c-234fc6fe6df1"), "00000000-0000-0000-0000-000000000000", "Vendor role", "Vendor", "VENDOR" },
                    { new Guid("f6fe96d6-874b-4c9e-a80e-b35d430029da"), "00000000-0000-0000-0000-000000000000", "Administrator role", "Admin", "ADMIN" }
                });
        }
    }
}
