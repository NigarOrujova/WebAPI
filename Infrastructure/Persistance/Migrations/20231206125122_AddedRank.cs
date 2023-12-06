using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedRank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Rank",
                table: "Teams",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Rank",
                table: "Customers",
                type: "smallint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Customers");
        }
    }
}
