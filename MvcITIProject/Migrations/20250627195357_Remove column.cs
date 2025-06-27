using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcITIProject.Migrations
{
    /// <inheritdoc />
    public partial class Removecolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MG_ID",
                table: "Floor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MG_ID",
                table: "Floor",
                type: "int",
                nullable: true);
        }
    }
}
