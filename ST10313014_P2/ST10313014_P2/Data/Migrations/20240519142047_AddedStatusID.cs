using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10313014_P2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedStatusID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "OrderStatus");

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "OrderStatus",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "OrderStatus");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "OrderStatus",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
