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
    [Migration("20240401145749_FixedJointTable")]
    partial class FixedJointTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CustomerGames", b =>
                {
                    b.Property<int>("CustomerModelId")
                        .HasColumnType("int");

                    b.Property<int>("GameModelId")
                        .HasColumnType("int");

                    b.HasKey("CustomerModelId", "GameModelId");

                    b.HasIndex("GameModelId");

                    b.ToTable("CustomerGames");

                    b.HasData(
                        new
                        {
                            CustomerModelId = 1,
                            GameModelId = 1
                        },
                        new
                        {
                            CustomerModelId = 1,
                            GameModelId = 3
                        });
                });

            modelBuilder.Entity("RetroRealm.Models.CountryModel", b =>
                {
                    b.Property<int>("CountryModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CountryModelId"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CountryModelId");

                    b.ToTable("Countries");

                    b.HasData(
                        new
                        {
                            CountryModelId = 1,
                            Country = "United States"
                        },
                        new
                        {
                            CountryModelId = 2,
                            Country = "Canada"
                        },
                        new
                        {
                            CountryModelId = 3,
                            Country = "United Kingdom"
                        },
                        new
                        {
                            CountryModelId = 4,
                            Country = "Germany"
                        },
                        new
                        {
                            CountryModelId = 5,
                            Country = "France"
                        },
                        new
                        {
                            CountryModelId = 6,
                            Country = "Australia"
                        },
                        new
                        {
                            CountryModelId = 7,
                            Country = "Japan"
                        },
                        new
                        {
                            CountryModelId = 8,
                            Country = "Brazil"
                        },
                        new
                        {
                            CountryModelId = 9,
                            Country = "India"
                        },
                        new
                        {
                            CountryModelId = 10,
                            Country = "South Africa"
                        });
                });

            modelBuilder.Entity("RetroRealm.Models.CustomerModel", b =>
                {
                    b.Property<int>("CustomerModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerModelId"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("CountryModelId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerModelId");

                    b.HasIndex("CountryModelId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerModelId = 1,
                            Address = "989 Beach Way",
                            City = "San Fransisco",
                            CountryModelId = 1,
                            Email = "ania@yahoo.com",
                            Firstname = "Ania",
                            Lastname = "Irvin",
                            Phone = "314-890-7889",
                            PostalCode = "94110",
                            State = "CA"
                        });
                });

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

            modelBuilder.Entity("RetroRealm.Models.IncidentModel", b =>
                {
                    b.Property<int>("IncidentModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IncidentModelId"));

                    b.Property<int?>("CustomerModelId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateClosed")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateOpened")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameModelId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("TechnicianModelId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncidentModelId");

                    b.HasIndex("CustomerModelId");

                    b.HasIndex("GameModelId");

                    b.HasIndex("TechnicianModelId");

                    b.ToTable("Incidents");

                    b.HasData(
                        new
                        {
                            IncidentModelId = 1,
                            CustomerModelId = 1,
                            DateOpened = new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "When I insert cartridge the game flashes and crashes",
                            GameModelId = 2,
                            TechnicianModelId = 3,
                            Title = "Error launching game"
                        });
                });

            modelBuilder.Entity("RetroRealm.Models.TechnicianModel", b =>
                {
                    b.Property<int>("TechnicianModelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TechnicianModelId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TechnicianModelId");

                    b.ToTable("Technicians");

                    b.HasData(
                        new
                        {
                            TechnicianModelId = -1,
                            Email = "",
                            Name = "Not Assigned",
                            Phone = ""
                        },
                        new
                        {
                            TechnicianModelId = 1,
                            Email = "alison@retrorealmsoftware.com",
                            Name = "Alison Diaz",
                            Phone = "800-555-0449"
                        },
                        new
                        {
                            TechnicianModelId = 2,
                            Email = "tonyc@retrorealmsoftware.com",
                            Name = "Tony Chef",
                            Phone = "314-123-4567"
                        },
                        new
                        {
                            TechnicianModelId = 3,
                            Email = "poynter@retrorealmsoftware.com",
                            Name = "Trey Poynter",
                            Phone = "573-789-1234"
                        },
                        new
                        {
                            TechnicianModelId = 4,
                            Email = "johnd@retrorealmsoftware.com",
                            Name = "John Doe",
                            Phone = "111-222-3333"
                        });
                });

            modelBuilder.Entity("CustomerGames", b =>
                {
                    b.HasOne("RetroRealm.Models.CustomerModel", null)
                        .WithMany()
                        .HasForeignKey("CustomerModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RetroRealm.Models.GameModel", null)
                        .WithMany()
                        .HasForeignKey("GameModelId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("RetroRealm.Models.CustomerModel", b =>
                {
                    b.HasOne("RetroRealm.Models.CountryModel", "CountryModel")
                        .WithMany()
                        .HasForeignKey("CountryModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CountryModel");
                });

            modelBuilder.Entity("RetroRealm.Models.IncidentModel", b =>
                {
                    b.HasOne("RetroRealm.Models.CustomerModel", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RetroRealm.Models.GameModel", "Game")
                        .WithMany()
                        .HasForeignKey("GameModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RetroRealm.Models.TechnicianModel", "Technician")
                        .WithMany()
                        .HasForeignKey("TechnicianModelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Game");

                    b.Navigation("Technician");
                });
#pragma warning restore 612, 618
        }
    }
}
