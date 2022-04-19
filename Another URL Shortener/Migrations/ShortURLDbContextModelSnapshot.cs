﻿// <auto-generated />
using System;
using Another_URL_Shortener.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Another_URL_SHortener.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ShortURLDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Another_URL_Shortener.Models.ShortUrl", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ShortedURL")
                        .HasColumnType("text");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ShortUrls");

                    b.HasData(
                        new
                        {
                            Id = new Guid("975a28e0-ad89-45e4-a906-e282a6c63e62"),
                            CreatedOn = new DateTime(2022, 4, 12, 12, 45, 0, 451, DateTimeKind.Local).AddTicks(66),
                            IsExpired = false,
                            ModifiedOn = new DateTime(2022, 4, 12, 12, 45, 0, 453, DateTimeKind.Local).AddTicks(1669),
                            ShortedURL = "https://blog.jetbrains.com/12345",
                            URL = "https://blog.jetbrains.com/dotnet/2020/11/25/getting-started-with-entity-framework-core-5/"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
