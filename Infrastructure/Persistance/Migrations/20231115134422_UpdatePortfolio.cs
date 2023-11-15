using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetaDescription",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescriptionAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeyword",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywordAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitle",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitleAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileTitle",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileTitleAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgDescription",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgDescriptionAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgTitle",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgTitleAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Slug",
                table: "Portfolios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SlugAz",
                table: "Portfolios",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(10000)", maxLength: 10000, nullable: false),
                    ImagePath = table.Column<string>(type: "text", nullable: false),
                    ImageAlt = table.Column<string>(type: "text", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    SlugAz = table.Column<string>(type: "text", nullable: false),
                    MetaKeyword = table.Column<string>(type: "text", nullable: true),
                    MetaKeywordAz = table.Column<string>(type: "text", nullable: true),
                    MetaTitle = table.Column<string>(type: "text", nullable: true),
                    MetaTitleAz = table.Column<string>(type: "text", nullable: true),
                    OgTitle = table.Column<string>(type: "text", nullable: true),
                    OgTitleAz = table.Column<string>(type: "text", nullable: true),
                    MetaDescription = table.Column<string>(type: "text", nullable: true),
                    MetaDescriptionAz = table.Column<string>(type: "text", nullable: true),
                    OgDescription = table.Column<string>(type: "text", nullable: true),
                    OgDescriptionAz = table.Column<string>(type: "text", nullable: true),
                    MobileTitle = table.Column<string>(type: "text", nullable: true),
                    MobileTitleAz = table.Column<string>(type: "text", nullable: true),
                    PublishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DeletedAt = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogTagCloud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BlogId = table.Column<int>(type: "integer", nullable: false),
                    TagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTagCloud", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogTagCloud_Blog_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTagCloud_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagCloud_BlogId",
                table: "BlogTagCloud",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTagCloud_TagId",
                table: "BlogTagCloud",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTagCloud");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropColumn(
                name: "MetaDescription",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MetaDescriptionAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MetaKeyword",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MetaKeywordAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MetaTitle",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MetaTitleAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MobileTitle",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "MobileTitleAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "OgDescription",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "OgDescriptionAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "OgTitle",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "OgTitleAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "Slug",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "SlugAz",
                table: "Portfolios");
        }
    }
}
