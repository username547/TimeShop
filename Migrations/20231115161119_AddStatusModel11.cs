using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeShop.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusModel11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Statuses",
                newName: "StatusName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusName",
                table: "Statuses",
                newName: "RoleName");
        }
    }
}
