using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class editEntityBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookFile",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookFile",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");
        }
    }
}
