using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShopeeFoodClone.WebApi.Products.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRatingColumnsForProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RateCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RateCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Products");
        }
    }
}
