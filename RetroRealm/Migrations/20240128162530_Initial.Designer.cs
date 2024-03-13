﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RetroRealm.Data;


#nullable disable

namespace RetroRealm.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240128162530_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RetroRealm.Models.GameModel", b =>
                {
                    b.Property<int>("GameModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameModelId"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Price")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateOnly?>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GameModelId");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            GameModelId = 1,
                            Code = "CNTPD91",
                            Price = 10m,
                            ReleaseDate = new DateOnly(1981, 6, 15),
                            Title = "Centipede"
                        },
                        new
                        {
                            GameModelId = 2,
                            Code = "ASTRD79",
                            Price = 20m,
                            ReleaseDate = new DateOnly(1979, 11, 17),
                            Title = "Asteroids"
                        },
                        new
                        {
                            GameModelId = 3,
                            Code = "ADVTR80",
                            Price = 20m,
                            ReleaseDate = new DateOnly(1980, 10, 12),
                            Title = "Adventure"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
