using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ventasAPI.Migrations
{
    /// <inheritdoc />
    public partial class InvoiceCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Invoices");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Invoices",
                type: "nvarchar(12)",
                maxLength: 12,
                nullable: false,
                defaultValue: "");
        }
    }
}
