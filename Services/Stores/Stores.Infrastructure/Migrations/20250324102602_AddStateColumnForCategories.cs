using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopeeFoodClone.WebApi.Stores.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStateColumnForCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "SubCategories",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "State",
                table: "Categories",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "SubCategories");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Categories");
        }
    }
}
