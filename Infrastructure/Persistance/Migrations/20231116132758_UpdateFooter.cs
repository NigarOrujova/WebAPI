using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFooter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTagCloud_Blog_BlogId",
                table: "BlogTagCloud");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTagCloud_Tag_TagId",
                table: "BlogTagCloud");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTagCloud",
                table: "BlogTagCloud");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blog",
                table: "Blog");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "BlogTagCloud",
                newName: "BlogTagClouds");

            migrationBuilder.RenameTable(
                name: "Blog",
                newName: "Blogs");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTagCloud_TagId",
                table: "BlogTagClouds",
                newName: "IX_BlogTagClouds_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTagCloud_BlogId",
                table: "BlogTagClouds",
                newName: "IX_BlogTagClouds_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_SlugAz",
                table: "Blogs",
                newName: "IX_Blogs_SlugAz");

            migrationBuilder.RenameIndex(
                name: "IX_Blog_Slug",
                table: "Blogs",
                newName: "IX_Blogs_Slug");

            migrationBuilder.AlterColumn<string>(
                name: "TitleAz",
                table: "Portfolios",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressAz",
                table: "Footers",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTagClouds",
                table: "BlogTagClouds",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTagClouds_Blogs_BlogId",
                table: "BlogTagClouds",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTagClouds_Tags_TagId",
                table: "BlogTagClouds",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTagClouds_Blogs_BlogId",
                table: "BlogTagClouds");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTagClouds_Tags_TagId",
                table: "BlogTagClouds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTagClouds",
                table: "BlogTagClouds");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Blogs",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "AddressAz",
                table: "Footers");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "BlogTagClouds",
                newName: "BlogTagCloud");

            migrationBuilder.RenameTable(
                name: "Blogs",
                newName: "Blog");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTagClouds_TagId",
                table: "BlogTagCloud",
                newName: "IX_BlogTagCloud_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTagClouds_BlogId",
                table: "BlogTagCloud",
                newName: "IX_BlogTagCloud_BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_SlugAz",
                table: "Blog",
                newName: "IX_Blog_SlugAz");

            migrationBuilder.RenameIndex(
                name: "IX_Blogs_Slug",
                table: "Blog",
                newName: "IX_Blog_Slug");

            migrationBuilder.AlterColumn<string>(
                name: "TitleAz",
                table: "Portfolios",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTagCloud",
                table: "BlogTagCloud",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Blog",
                table: "Blog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTagCloud_Blog_BlogId",
                table: "BlogTagCloud",
                column: "BlogId",
                principalTable: "Blog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTagCloud_Tag_TagId",
                table: "BlogTagCloud",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
