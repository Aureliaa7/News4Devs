using Microsoft.EntityFrameworkCore.Migrations;

namespace News4Devs.Infrastructure.Migrations
{
    public partial class ModifiedArticlesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ReadingTimeMinutes",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "SocialImageUrl",
                table: "Articles",
                newName: "UrlToImage");

            migrationBuilder.RenameColumn(
                name: "ReadablePublishDate",
                table: "Articles",
                newName: "Content");

            migrationBuilder.RenameColumn(
                name: "AuthorWebsiteUrl",
                table: "Articles",
                newName: "Author");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UrlToImage",
                table: "Articles",
                newName: "SocialImageUrl");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Articles",
                newName: "ReadablePublishDate");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Articles",
                newName: "AuthorWebsiteUrl");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Articles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReadingTimeMinutes",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Articles",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");
        }
    }
}
