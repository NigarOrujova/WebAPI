using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adddmanytomanyportfolioandcategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Portfolios_PortfolioId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_PortfolioId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "PortfolioId",
                table: "Categories");

            migrationBuilder.CreateTable(
                name: "PortfolioCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortfolioId = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    DeletedAt = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortfolioCategory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PortfolioCategory_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PortfolioCategory_Portfolios_PortfolioId",
                        column: x => x.PortfolioId,
                        principalTable: "Portfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCategory_CategoryId",
                table: "PortfolioCategory",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioCategory_PortfolioId",
                table: "PortfolioCategory",
                column: "PortfolioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PortfolioCategory");

            migrationBuilder.AddColumn<int>(
                name: "PortfolioId",
                table: "Categories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_PortfolioId",
                table: "Categories",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Portfolios_PortfolioId",
                table: "Categories",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id");
        }
    }
}
