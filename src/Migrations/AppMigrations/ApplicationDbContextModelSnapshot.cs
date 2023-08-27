﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PingPongTracker.Data;

#nullable disable

namespace PingPongTracker.Migrations.AppMigrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PingPongTracker.Models.Game", b =>
                {
                    b.Property<Guid>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("MatchupDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Player1WinnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Player2WinnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Team1Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Team1Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Team1Score")
                        .HasColumnType("int");

                    b.Property<Guid>("Team2Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Team2Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Team2Score")
                        .HasColumnType("int");

                    b.Property<Guid>("TournamentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GameId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PingPongTracker.Models.Player", b =>
                {
                    b.Property<Guid>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<bool>("Eligible")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("TournamentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

                    b.HasIndex("TournamentId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("PingPongTracker.Models.Season", b =>
                {
                    b.Property<Guid>("SeasonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Active")
                        .HasColumnType("bit");

                    b.Property<string>("SeasonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SeasonStart")
                        .HasColumnType("datetime2");

                    b.HasKey("SeasonId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("PingPongTracker.Models.Tournament", b =>
                {
                    b.Property<Guid>("TournamentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("SeasonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("TournamentDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("TournamentEndDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TournamentId");

                    b.HasIndex("SeasonId");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("PingPongTracker.Models.Game", b =>
                {
                    b.HasOne("PingPongTracker.Models.Tournament", "Tournament")
                        .WithMany()
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("PingPongTracker.Models.Player", b =>
                {
                    b.HasOne("PingPongTracker.Models.Tournament", null)
                        .WithMany("Players")
                        .HasForeignKey("TournamentId");
                });

            modelBuilder.Entity("PingPongTracker.Models.Tournament", b =>
                {
                    b.HasOne("PingPongTracker.Models.Season", null)
                        .WithMany("Tournaments")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("PingPongTracker.Models.Season", b =>
                {
                    b.Navigation("Tournaments");
                });

            modelBuilder.Entity("PingPongTracker.Models.Tournament", b =>
                {
                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
