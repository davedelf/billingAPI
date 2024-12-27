using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventasAPI.Migrations
{
    /// <inheritdoc />
    public partial class UserImageURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Sellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Customers");
        }
    }
}
