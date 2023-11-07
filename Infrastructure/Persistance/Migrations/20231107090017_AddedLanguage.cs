using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppNameAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescriptionAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywordAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitleAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileTitleAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgDescriptionAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgSiteNameAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgTitleAz",
                table: "We",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FulllNameAz",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageAltAz",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobAz",
                table: "Teams",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTitleAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleAz",
                table: "Portfolios",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "PortfolioImages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ImageAlt",
                table: "PortfolioImages",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ImageAltAz",
                table: "PortfolioImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionAz",
                table: "OurValues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TitleAz",
                table: "OurValues",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppNameAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescriptionAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywordAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitleAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileTitleAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgDescriptionAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgSiteNameAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgTitleAz",
                table: "Love",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppNameAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescriptionAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywordAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitleAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileTitleAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgDescriptionAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgSiteNameAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgTitleAz",
                table: "Home",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppNameAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescriptionAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaKeywordAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitleAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileTitleAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgDescriptionAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgSiteNameAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgTitleAz",
                table: "Esc",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "ImageAlt",
                table: "Customers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ImageAltAz",
                table: "Customers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppNameAz",
                table: "Contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaDescriptionAz",
                table: "Contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MetaTitleAz",
                table: "Contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileTitleAz",
                table: "Contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgDescriptionAz",
                table: "Contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgSiteNameAz",
                table: "Contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OgTitleAz",
                table: "Contact",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameAz",
                table: "Categories",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppNameAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "MetaDescriptionAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "MetaKeywordAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "MetaTitleAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "MobileTitleAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "OgDescriptionAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "OgSiteNameAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "OgTitleAz",
                table: "We");

            migrationBuilder.DropColumn(
                name: "FulllNameAz",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "ImageAltAz",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "JobAz",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "DescriptionAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "SubTitleAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "TitleAz",
                table: "Portfolios");

            migrationBuilder.DropColumn(
                name: "ImageAltAz",
                table: "PortfolioImages");

            migrationBuilder.DropColumn(
                name: "DescriptionAz",
                table: "OurValues");

            migrationBuilder.DropColumn(
                name: "TitleAz",
                table: "OurValues");

            migrationBuilder.DropColumn(
                name: "AppNameAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "MetaDescriptionAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "MetaKeywordAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "MetaTitleAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "MobileTitleAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "OgDescriptionAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "OgSiteNameAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "OgTitleAz",
                table: "Love");

            migrationBuilder.DropColumn(
                name: "AppNameAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "MetaDescriptionAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "MetaKeywordAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "MetaTitleAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "MobileTitleAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "OgDescriptionAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "OgSiteNameAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "OgTitleAz",
                table: "Home");

            migrationBuilder.DropColumn(
                name: "AppNameAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "MetaDescriptionAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "MetaKeywordAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "MetaTitleAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "MobileTitleAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "OgDescriptionAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "OgSiteNameAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "OgTitleAz",
                table: "Esc");

            migrationBuilder.DropColumn(
                name: "ImageAltAz",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "AppNameAz",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MetaDescriptionAz",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MetaTitleAz",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "MobileTitleAz",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "OgDescriptionAz",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "OgSiteNameAz",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "OgTitleAz",
                table: "Contact");

            migrationBuilder.DropColumn(
                name: "NameAz",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "PortfolioImages",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageAlt",
                table: "PortfolioImages",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImageAlt",
                table: "Customers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
