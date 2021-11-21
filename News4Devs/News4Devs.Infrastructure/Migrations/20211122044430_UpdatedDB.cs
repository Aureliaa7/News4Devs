using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace News4Devs.Infrastructure.Migrations
{
    public partial class UpdatedDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_DevUsers_DevUserId",
                table: "Articles");

            migrationBuilder.DropTable(
                name: "DevUsers");

            migrationBuilder.DropIndex(
                name: "IX_Articles_DevUserId",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "DevUserId",
                table: "Articles");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Articles",
                type: "nvarchar(40)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AuthorWebsiteUrl",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "AuthorWebsiteUrl",
                table: "Articles");

            migrationBuilder.AddColumn<Guid>(
                name: "DevUserId",
                table: "Articles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DevUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebsiteUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DevUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_DevUserId",
                table: "Articles",
                column: "DevUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_DevUsers_DevUserId",
                table: "Articles",
                column: "DevUserId",
                principalTable: "DevUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
