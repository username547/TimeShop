using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeShop.Migrations
{
    /// <inheritdoc />
    public partial class Mega1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImagePath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImagePath",
                table: "Products");
        }
    }
}
