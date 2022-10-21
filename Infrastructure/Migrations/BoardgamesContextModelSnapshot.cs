﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(BoardgamesContext))]
    partial class BoardgamesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Core.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Address");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "Breda",
                            HouseNumber = 97,
                            PostalCode = "4814AE",
                            StreetName = "Tramsingel"
                        },
                        new
                        {
                            Id = 2,
                            City = "Rotterdam",
                            HouseNumber = 199,
                            PostalCode = "5317MJ",
                            StreetName = "Pleinweg"
                        },
                        new
                        {
                            Id = 3,
                            City = "Devtown",
                            HouseNumber = 52,
                            PostalCode = "4452SG",
                            StreetName = "Teststraat"
                        });
                });

            modelBuilder.Entity("Core.Domain.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("AdultsOnly")
                        .HasColumnType("bit");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GameImageId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("category")
                        .HasColumnType("int");

                    b.Property<int>("genre")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Game");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AdultsOnly = false,
                            Description = "It's me Mario",
                            Name = "MarioKart",
                            category = 2,
                            genre = 10
                        },
                        new
                        {
                            Id = 2,
                            AdultsOnly = false,
                            Description = "Can you buy all the houses?",
                            Name = "Monopoly",
                            category = 1,
                            genre = 6
                        },
                        new
                        {
                            Id = 3,
                            AdultsOnly = false,
                            Description = "Scribble, Scrubble, Scrabble",
                            Name = "Scrabble",
                            category = 1,
                            genre = 7
                        },
                        new
                        {
                            Id = 4,
                            AdultsOnly = true,
                            Description = "How offensive can you get?",
                            Name = "Cards against humanity",
                            category = 0,
                            genre = 3
                        },
                        new
                        {
                            Id = 5,
                            AdultsOnly = true,
                            Description = "Nice hand bro",
                            Name = "Poker",
                            category = 0,
                            genre = 7
                        },
                        new
                        {
                            Id = 6,
                            AdultsOnly = true,
                            Description = "Dont hit at 21",
                            Name = "Blackjack",
                            category = 0,
                            genre = 7
                        });
                });

            modelBuilder.Entity("Core.Domain.GameImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("GameID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("PictureFormat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameID")
                        .IsUnique()
                        .HasFilter("[GameID] IS NOT NULL");

                    b.ToTable("GameImage");
                });

            modelBuilder.Entity("Core.Domain.GameNight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<bool>("AdultsOnly")
                        .HasColumnType("bit");

                    b.Property<bool>("AlcoholFree")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LactoseIntolerant")
                        .HasColumnType("bit");

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NutAllergy")
                        .HasColumnType("bit");

                    b.Property<int>("OrganiserId")
                        .HasColumnType("int");

                    b.Property<bool>("Vegan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("OrganiserId");

                    b.ToTable("GameNight");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            AdultsOnly = false,
                            AlcoholFree = false,
                            DateTime = new DateTime(2022, 10, 21, 9, 6, 45, 932, DateTimeKind.Local).AddTicks(3410),
                            LactoseIntolerant = false,
                            MaxPlayers = 6,
                            Name = "MarioKart session",
                            NutAllergy = false,
                            OrganiserId = 1,
                            Vegan = false
                        },
                        new
                        {
                            Id = 2,
                            AddressId = 3,
                            AdultsOnly = true,
                            AlcoholFree = false,
                            DateTime = new DateTime(2022, 10, 21, 9, 6, 45, 932, DateTimeKind.Local).AddTicks(3480),
                            LactoseIntolerant = false,
                            MaxPlayers = 6,
                            Name = "Poker night",
                            NutAllergy = false,
                            OrganiserId = 2,
                            Vegan = false
                        });
                });

            modelBuilder.Entity("Core.Domain.GameNightGame", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("GameNightId")
                        .HasColumnType("int");

                    b.HasKey("GameId", "GameNightId");

                    b.HasIndex("GameNightId");

                    b.ToTable("GameNight_Game");

                    b.HasData(
                        new
                        {
                            GameId = 1,
                            GameNightId = 1
                        });
                });

            modelBuilder.Entity("Core.Domain.GameNightPlayer", b =>
                {
                    b.Property<int>("GameNightId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("GameNightId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("GameNight_Player");

                    b.HasData(
                        new
                        {
                            GameNightId = 1,
                            PersonId = 1
                        },
                        new
                        {
                            GameNightId = 1,
                            PersonId = 2
                        },
                        new
                        {
                            GameNightId = 1,
                            PersonId = 3
                        });
                });

            modelBuilder.Entity("Core.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<bool>("AlcoholFree")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("LactoseIntolerant")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoShows")
                        .HasColumnType("int");

                    b.Property<bool>("NutAllergy")
                        .HasColumnType("bit");

                    b.Property<int>("Shows")
                        .HasColumnType("int");

                    b.Property<bool>("Vegan")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Person");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AddressId = 1,
                            AlcoholFree = false,
                            DateOfBirth = new DateTime(1998, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "boris@email.com",
                            Gender = 0,
                            LactoseIntolerant = false,
                            Name = "Boris Pouw",
                            NoShows = 0,
                            NutAllergy = true,
                            Shows = 0,
                            Vegan = false
                        },
                        new
                        {
                            Id = 2,
                            AddressId = 2,
                            AlcoholFree = false,
                            DateOfBirth = new DateTime(1999, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "ntstefi@email.com",
                            Gender = 1,
                            LactoseIntolerant = false,
                            Name = "Stefi Nicoara",
                            NoShows = 0,
                            NutAllergy = false,
                            Shows = 0,
                            Vegan = true
                        },
                        new
                        {
                            Id = 3,
                            AddressId = 3,
                            AlcoholFree = true,
                            DateOfBirth = new DateTime(2000, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "piet@email.com",
                            Gender = 0,
                            LactoseIntolerant = true,
                            Name = "Piet Test",
                            NoShows = 0,
                            NutAllergy = true,
                            Shows = 0,
                            Vegan = true
                        });
                });

            modelBuilder.Entity("Core.Domain.GameImage", b =>
                {
                    b.HasOne("Core.Domain.Game", "Game")
                        .WithOne("GameImage")
                        .HasForeignKey("Core.Domain.GameImage", "GameID")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Game");
                });

            modelBuilder.Entity("Core.Domain.GameNight", b =>
                {
                    b.HasOne("Core.Domain.Address", "Address")
                        .WithMany("GameNights")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Person", "Organiser")
                        .WithMany("OrganisedGameNights")
                        .HasForeignKey("OrganiserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Organiser");
                });

            modelBuilder.Entity("Core.Domain.GameNightGame", b =>
                {
                    b.HasOne("Core.Domain.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.GameNight", "GameNight")
                        .WithMany()
                        .HasForeignKey("GameNightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("GameNight");
                });

            modelBuilder.Entity("Core.Domain.GameNightPlayer", b =>
                {
                    b.HasOne("Core.Domain.GameNight", "GameNight")
                        .WithMany()
                        .HasForeignKey("GameNightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GameNight");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("Core.Domain.Person", b =>
                {
                    b.HasOne("Core.Domain.Address", "Address")
                        .WithMany("Persons")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Core.Domain.Address", b =>
                {
                    b.Navigation("GameNights");

                    b.Navigation("Persons");
                });

            modelBuilder.Entity("Core.Domain.Game", b =>
                {
                    b.Navigation("GameImage")
                        .IsRequired();
                });

            modelBuilder.Entity("Core.Domain.Person", b =>
                {
                    b.Navigation("OrganisedGameNights");
                });
#pragma warning restore 612, 618
        }
    }
}
