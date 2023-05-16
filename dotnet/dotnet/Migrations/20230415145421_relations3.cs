using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet.Migrations
{
    /// <inheritdoc />
    public partial class relations3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "Details");

            migrationBuilder.AddColumn<int>(
                name: "BirthYear",
                table: "Details",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BirthYear",
                table: "Details");

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "Details",
                type: "datetime2",
                nullable: true);
        }
    }
}
