using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorBootcamp_DataAccess.Migrations
{
    public partial class UpdateProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShopFavourites",
                table: "Products",
                newName: "ShopFavorites");

            migrationBuilder.AddColumn<bool>(
                name: "CustomerFavorites",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerFavorites",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ShopFavorites",
                table: "Products",
                newName: "ShopFavourites");
        }
    }
}
