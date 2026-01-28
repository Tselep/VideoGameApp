using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameApp.Migrations
{
    /// <inheritdoc />
    public partial class RenameOrderTotalToTotalPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GameId",
                table: "OrderItems",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Games_GameId",
                table: "OrderItems",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Games_GameId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_GameId",
                table: "OrderItems");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "Total");
        }
    }
}
