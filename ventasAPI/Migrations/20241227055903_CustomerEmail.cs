using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventasAPI.Migrations
{
    /// <inheritdoc />
    public partial class CustomerEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sellers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Customers",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Customers");
        }
    }
}
