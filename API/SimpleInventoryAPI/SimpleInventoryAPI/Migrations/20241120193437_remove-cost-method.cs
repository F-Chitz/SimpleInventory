using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleInventoryAPI.Migrations
{
    /// <inheritdoc />
    public partial class removecostmethod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostMethod",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CostMethod",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
