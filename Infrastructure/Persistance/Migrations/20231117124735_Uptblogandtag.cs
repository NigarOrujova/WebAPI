using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Uptblogandtag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Blogs_SlugAz",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "SlugAz",
                table: "Blogs",
                newName: "TitleAz");

            migrationBuilder.AddColumn<string>(
                name: "NameAz",
                table: "Tags",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAz",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageAltAz",
                table: "Blogs",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameAz",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DescriptionAz",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ImageAltAz",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "TitleAz",
                table: "Blogs",
                newName: "SlugAz");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_SlugAz",
                table: "Blogs",
                column: "SlugAz",
                unique: true);
        }
    }
}
