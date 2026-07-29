using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBorrowTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowDate",
                table: "Newspapers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedByEmail",
                table: "Newspapers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedByUserId",
                table: "Newspapers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Newspapers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowDate",
                table: "Magazines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedByEmail",
                table: "Magazines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedByUserId",
                table: "Magazines",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Magazines",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BorrowDate",
                table: "Books",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedByEmail",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowedByUserId",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "Books",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BorrowDate",
                table: "Newspapers");

            migrationBuilder.DropColumn(
                name: "BorrowedByEmail",
                table: "Newspapers");

            migrationBuilder.DropColumn(
                name: "BorrowedByUserId",
                table: "Newspapers");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Newspapers");

            migrationBuilder.DropColumn(
                name: "BorrowDate",
                table: "Magazines");

            migrationBuilder.DropColumn(
                name: "BorrowedByEmail",
                table: "Magazines");

            migrationBuilder.DropColumn(
                name: "BorrowedByUserId",
                table: "Magazines");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Magazines");

            migrationBuilder.DropColumn(
                name: "BorrowDate",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BorrowedByEmail",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BorrowedByUserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "Books");
        }
    }
}
