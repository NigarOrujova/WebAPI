﻿// <auto-generated />
using System;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(YelloadDbContext))]
    [Migration("20231107105709_AddedEmp")]
    partial class AddedEmp
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("NameAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<string>("AppNameAz")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("MetaDescription")
                        .HasColumnType("text");

                    b.Property<string>("MetaDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitle")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("OgDescription")
                        .HasColumnType("text");

                    b.Property<string>("OgDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteName")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteNameAz")
                        .HasColumnType("text");

                    b.Property<string>("OgTitle")
                        .HasColumnType("text");

                    b.Property<string>("OgTitleAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("Domain.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageAlt")
                        .HasColumnType("text");

                    b.Property<string>("ImageAltAz")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("Domain.Entities.EmployeesPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Description2")
                        .HasColumnType("text");

                    b.Property<string>("DescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("DescriptionAz2")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .HasColumnType("text");

                    b.Property<string>("FullName2")
                        .HasColumnType("text");

                    b.Property<string>("FullNameAz")
                        .HasColumnType("text");

                    b.Property<string>("FullNameAz2")
                        .HasColumnType("text");

                    b.Property<string>("ImageAlt")
                        .HasColumnType("text");

                    b.Property<string>("ImageAlt2")
                        .HasColumnType("text");

                    b.Property<string>("ImageAltAz")
                        .HasColumnType("text");

                    b.Property<string>("ImageAltAz2")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath2")
                        .HasColumnType("text");

                    b.Property<string>("SubTitle")
                        .HasColumnType("text");

                    b.Property<string>("SubTitle2")
                        .HasColumnType("text");

                    b.Property<string>("SubTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("SubTitleAz2")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("Title2")
                        .HasColumnType("text");

                    b.Property<string>("Title3")
                        .HasColumnType("text");

                    b.Property<string>("TitleAz")
                        .HasColumnType("text");

                    b.Property<string>("TitleAz2")
                        .HasColumnType("text");

                    b.Property<string>("TitleAz3")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("EmployeesPages");
                });

            modelBuilder.Entity("Domain.Entities.Esc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<string>("AppNameAz")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("MetaDescription")
                        .HasColumnType("text");

                    b.Property<string>("MetaDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeyword")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeywordAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitle")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("OgDescription")
                        .HasColumnType("text");

                    b.Property<string>("OgDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteName")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteNameAz")
                        .HasColumnType("text");

                    b.Property<string>("OgTitle")
                        .HasColumnType("text");

                    b.Property<string>("OgTitleAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Esc");
                });

            modelBuilder.Entity("Domain.Entities.Footer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.HasKey("Id");

                    b.ToTable("Footers");
                });

            modelBuilder.Entity("Domain.Entities.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<string>("AppNameAz")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("MetaDescription")
                        .HasColumnType("text");

                    b.Property<string>("MetaDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeyword")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeywordAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitle")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("OgDescription")
                        .HasColumnType("text");

                    b.Property<string>("OgDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteName")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteNameAz")
                        .HasColumnType("text");

                    b.Property<string>("OgTitle")
                        .HasColumnType("text");

                    b.Property<string>("OgTitleAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Home");
                });

            modelBuilder.Entity("Domain.Entities.Love", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<string>("AppNameAz")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("MetaDescription")
                        .HasColumnType("text");

                    b.Property<string>("MetaDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeyword")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeywordAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitle")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("OgDescription")
                        .HasColumnType("text");

                    b.Property<string>("OgDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteName")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteNameAz")
                        .HasColumnType("text");

                    b.Property<string>("OgTitle")
                        .HasColumnType("text");

                    b.Property<string>("OgTitleAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Love");
                });

            modelBuilder.Entity("Domain.Entities.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<int?>("FooterId")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("FooterId");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<byte>("Rank")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.OurValue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("DescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("TitleAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OurValues");
                });

            modelBuilder.Entity("Domain.Entities.Portfolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(10000)
                        .HasColumnType("character varying(10000)");

                    b.Property<string>("DescriptionAz")
                        .HasColumnType("text");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<string>("SubTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("SubTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("TitleAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Portfolios");
                });

            modelBuilder.Entity("Domain.Entities.PortfolioCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<int>("PortfolioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PortfolioCategory");
                });

            modelBuilder.Entity("Domain.Entities.PortfolioImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageAlt")
                        .HasColumnType("text");

                    b.Property<string>("ImageAltAz")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<bool>("IsMain")
                        .HasColumnType("boolean");

                    b.Property<int?>("PortfolioId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("PortfolioId");

                    b.ToTable("PortfolioImages");
                });

            modelBuilder.Entity("Domain.Entities.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("FulllName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("FulllNameAz")
                        .HasColumnType("text");

                    b.Property<string>("ImageAlt")
                        .HasColumnType("text");

                    b.Property<string>("ImageAltAz")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath")
                        .HasColumnType("text");

                    b.Property<string>("ImagePath2")
                        .HasColumnType("text");

                    b.Property<string>("Job")
                        .HasColumnType("text");

                    b.Property<string>("JobAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Domain.Entities.We", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AppName")
                        .HasColumnType("text");

                    b.Property<string>("AppNameAz")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("DeletedAt")
                        .HasColumnType("boolean");

                    b.Property<string>("MetaDescription")
                        .HasColumnType("text");

                    b.Property<string>("MetaDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeyword")
                        .HasColumnType("text");

                    b.Property<string>("MetaKeywordAz")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitle")
                        .HasColumnType("text");

                    b.Property<string>("MetaTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitle")
                        .HasColumnType("text");

                    b.Property<string>("MobileTitleAz")
                        .HasColumnType("text");

                    b.Property<string>("OgDescription")
                        .HasColumnType("text");

                    b.Property<string>("OgDescriptionAz")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteName")
                        .HasColumnType("text");

                    b.Property<string>("OgSiteNameAz")
                        .HasColumnType("text");

                    b.Property<string>("OgTitle")
                        .HasColumnType("text");

                    b.Property<string>("OgTitleAz")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("We");
                });

            modelBuilder.Entity("Domain.Entities.Media", b =>
                {
                    b.HasOne("Domain.Entities.Footer", "Footer")
                        .WithMany("Medias")
                        .HasForeignKey("FooterId");

                    b.Navigation("Footer");
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppRoleClaim", b =>
                {
                    b.HasOne("Domain.Entities.Membership.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserClaim", b =>
                {
                    b.HasOne("Domain.Entities.Membership.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserLogin", b =>
                {
                    b.HasOne("Domain.Entities.Membership.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserRole", b =>
                {
                    b.HasOne("Domain.Entities.Membership.AppRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Membership.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.Membership.AppUserToken", b =>
                {
                    b.HasOne("Domain.Entities.Membership.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entities.PortfolioCategory", b =>
                {
                    b.HasOne("Domain.Entities.Category", "Category")
                        .WithMany("PortfolioCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Portfolio", "Portfolio")
                        .WithMany("PortfolioCategories")
                        .HasForeignKey("PortfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Domain.Entities.PortfolioImage", b =>
                {
                    b.HasOne("Domain.Entities.Portfolio", "Portfolio")
                        .WithMany("Images")
                        .HasForeignKey("PortfolioId");

                    b.Navigation("Portfolio");
                });

            modelBuilder.Entity("Domain.Entities.Category", b =>
                {
                    b.Navigation("PortfolioCategories");
                });

            modelBuilder.Entity("Domain.Entities.Footer", b =>
                {
                    b.Navigation("Medias");
                });

            modelBuilder.Entity("Domain.Entities.Portfolio", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("PortfolioCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
