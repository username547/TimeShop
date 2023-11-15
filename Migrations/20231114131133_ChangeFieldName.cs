using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeShop.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImagePath",
                table: "Products",
                newName: "ProductImage");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImage",
                table: "Products",
                newName: "ProductImagePath");
        }
    }
}
