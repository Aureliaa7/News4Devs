using Microsoft.EntityFrameworkCore.Migrations;

namespace News4Devs.Infrastructure.Migrations
{
    public partial class UpdatedUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePhotoPath",
                table: "ApplicationUsers",
                newName: "ProfilePhotoName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfilePhotoName",
                table: "ApplicationUsers",
                newName: "ProfilePhotoPath");
        }
    }
}
