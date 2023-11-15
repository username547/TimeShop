using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeShop.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "StatusModel",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusModel", x => x.StatusId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_StatusId",
                table: "Orders",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StatusModel_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "StatusModel",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StatusModel_StatusId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "StatusModel");

            migrationBuilder.DropIndex(
                name: "IX_Orders_StatusId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "Orders",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }
    }
}
