using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedEmp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeesPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    TitleAz = table.Column<string>(type: "text", nullable: true),
                    SubTitle = table.Column<string>(type: "text", nullable: true),
                    SubTitleAz = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DescriptionAz = table.Column<string>(type: "text", nullable: true),
                    Title2 = table.Column<string>(type: "text", nullable: true),
                    TitleAz2 = table.Column<string>(type: "text", nullable: true),
                    SubTitle2 = table.Column<string>(type: "text", nullable: true),
                    SubTitleAz2 = table.Column<string>(type: "text", nullable: true),
                    Title3 = table.Column<string>(type: "text", nullable: true),
                    TitleAz3 = table.Column<string>(type: "text", nullable: true),
                    Description2 = table.Column<string>(type: "text", nullable: true),
                    DescriptionAz2 = table.Column<string>(type: "text", nullable: true),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    FullNameAz = table.Column<string>(type: "text", nullable: true),
                    ImagePath = table.Column<string>(type: "text", nullable: true),
                    ImageAlt = table.Column<string>(type: "text", nullable: true),
                    ImageAltAz = table.Column<string>(type: "text", nullable: true),
                    FullName2 = table.Column<string>(type: "text", nullable: true),
                    FullNameAz2 = table.Column<string>(type: "text", nullable: true),
                    ImagePath2 = table.Column<string>(type: "text", nullable: true),
                    ImageAlt2 = table.Column<string>(type: "text", nullable: true),
                    ImageAltAz2 = table.Column<string>(type: "text", nullable: true),
                    DeletedAt = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesPages", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesPages");
        }
    }
}
