using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.BERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class addCoulmFinishDateInEntityRead : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FinishDate",
                table: "Reads",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinishDate",
                table: "Reads");
        }
    }
}
