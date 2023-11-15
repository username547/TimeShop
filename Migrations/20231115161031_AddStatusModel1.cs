using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeShop.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusModel1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_StatusModel_StatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StatusModel",
                table: "StatusModel");

            migrationBuilder.RenameTable(
                name: "StatusModel",
                newName: "Statuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Statuses_StatusId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Statuses",
                table: "Statuses");

            migrationBuilder.RenameTable(
                name: "Statuses",
                newName: "StatusModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StatusModel",
                table: "StatusModel",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_StatusModel_StatusId",
                table: "Orders",
                column: "StatusId",
                principalTable: "StatusModel",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
