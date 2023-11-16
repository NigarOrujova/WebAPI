using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UptPortfolioCon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_Slug",
                table: "Portfolios",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Portfolios_SlugAz",
                table: "Portfolios",
                column: "SlugAz",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_Slug",
                table: "Blog",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Blog_SlugAz",
                table: "Blog",
                column: "SlugAz",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Portfolios_Slug",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Portfolios_SlugAz",
                table: "Portfolios");

            migrationBuilder.DropIndex(
                name: "IX_Blog_Slug",
                table: "Blog");

            migrationBuilder.DropIndex(
                name: "IX_Blog_SlugAz",
                table: "Blog");
        }
    }
}
