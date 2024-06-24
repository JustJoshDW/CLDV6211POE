using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10313014_P2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Product_ProductID",
                table: "CartDetail");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "CartDetail",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_ProductID",
                table: "CartDetail",
                newName: "IX_CartDetail_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Product_ProductId",
                table: "CartDetail",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Product_ProductId",
                table: "CartDetail");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartDetail",
                newName: "ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_CartDetail_ProductId",
                table: "CartDetail",
                newName: "IX_CartDetail_ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Product_ProductID",
                table: "CartDetail",
                column: "ProductID",
                principalTable: "Product",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
