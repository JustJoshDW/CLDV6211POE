using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10313014_P2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedQuantityToOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "CartDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "CartDetail");
        }
    }
}
