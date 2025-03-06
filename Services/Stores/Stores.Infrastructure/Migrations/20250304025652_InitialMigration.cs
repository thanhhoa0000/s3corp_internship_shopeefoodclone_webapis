using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Categories",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                ConcurrencyStamp = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Categories", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Stores",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                OpeningHour = table.Column<TimeOnly>(type: "time", nullable: false),
                ClosingHour = table.Column<TimeOnly>(type: "time", nullable: false),
                CoverImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Rating = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Stores", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CategoryStore",
            columns: table => new
            {
                CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                StoresId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CategoryStore", x => new { x.CategoriesId, x.StoresId });
                table.ForeignKey(
                    name: "FK_CategoryStore_Categories_CategoriesId",
                    column: x => x.CategoriesId,
                    principalTable: "Categories",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_CategoryStore_Stores_StoresId",
                    column: x => x.StoresId,
                    principalTable: "Stores",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Categories_Name",
            table: "Categories",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_CategoryStore_StoresId",
            table: "CategoryStore",
            column: "StoresId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CategoryStore");

        migrationBuilder.DropTable(
            name: "Categories");

        migrationBuilder.DropTable(
            name: "Stores");
    }
}
