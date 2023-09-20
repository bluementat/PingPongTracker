﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PingPongTracker.Data;

#nullable disable

namespace PingPongTracker.Migrations.AppMigrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230920005935_ExpandGames")]
    partial class ExpandGames
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<Guid>("SeasonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Team1Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Team1Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Team1Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Team1Score")
                        .HasColumnType("int");

                    b.Property<string>("Team2Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Team2Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Team2Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Team2Score")
                        .HasColumnType("int");

                    b.HasKey("GameId");

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

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PlayerId");

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

            modelBuilder.Entity("PingPongTracker.Models.Team", b =>
                {
                    b.Property<int>("TeamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TeamID"));

                    b.Property<Guid>("Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Player1UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Player2UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TeamID");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("PingPongTracker.Models.TourneyGame", b =>
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

                    b.Property<Guid>("SeasonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Team1Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Team1Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Team1Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Team1Score")
                        .HasColumnType("int");

                    b.Property<string>("Team2Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Team2Player1Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("Team2Player2Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Team2Score")
                        .HasColumnType("int");

                    b.HasKey("GameId");

                    b.ToTable("TourneyGames");
                });
#pragma warning restore 612, 618
        }
    }
}
