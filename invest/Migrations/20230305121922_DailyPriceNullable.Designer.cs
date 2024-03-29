﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using invest.Model;

#nullable disable

namespace invest.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230305121922_DailyPriceNullable")]
    partial class DailyPriceNullable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("invest.Model.Cookie", b =>
                {
                    b.Property<int>("CookieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("CookieId"));

                    b.Property<DateTime>("Expires")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("CookieId");

                    b.ToTable("Cookies");

                    b.HasData(
                        new
                        {
                            CookieId = 1,
                            Expires = new DateTime(2023, 3, 11, 12, 19, 21, 932, DateTimeKind.Utc).AddTicks(5269),
                            Name = "steamLoginSecure",
                            Value = "76561199480411558%7C%7C4ADF3CEAC29FEDE4F05761F7FB323DAFDE517DC2"
                        },
                        new
                        {
                            CookieId = 2,
                            Expires = new DateTime(2023, 3, 11, 12, 19, 21, 932, DateTimeKind.Utc).AddTicks(5368),
                            Name = "sessionid",
                            Value = "f4aead9f9d5fb574d07c665c"
                        });
                });

            modelBuilder.Entity("invest.Model.Daily", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("ItemId")
                        .HasColumnType("integer");

                    b.Property<double>("MedianPrice")
                        .HasColumnType("double precision");

                    b.Property<double?>("Price")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Volume")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Daily");
                });

            modelBuilder.Entity("invest.Model.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ItemId"));

                    b.Property<int>("BuyAmount")
                        .HasColumnType("integer");

                    b.Property<double>("BuyPrice")
                        .HasColumnType("double precision");

                    b.Property<int>("Currency")
                        .HasColumnType("integer");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("ItemId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            ItemId = 1,
                            BuyAmount = 40,
                            BuyPrice = 0.97999999999999998,
                            Currency = 6,
                            Hash = "Antwerp%202022%20Legends%20Sticker%20Capsule",
                            Name = "Antwerp 2022 Legends Sticker Capsule",
                            Order = 0
                        },
                        new
                        {
                            ItemId = 2,
                            BuyAmount = 3,
                            BuyPrice = 0.0,
                            Currency = 6,
                            Hash = "Operation%20Hydra%20Case",
                            Name = "Operation Hydra Case",
                            Order = 1
                        });
                });

            modelBuilder.Entity("invest.Model.Point", b =>
                {
                    b.Property<int>("PointId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PointId"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ItemId")
                        .HasColumnType("integer");

                    b.Property<double>("Value")
                        .HasColumnType("double precision");

                    b.Property<int>("Volume")
                        .HasColumnType("integer");

                    b.HasKey("PointId");

                    b.HasIndex("ItemId");

                    b.ToTable("Points");
                });

            modelBuilder.Entity("invest.Model.Daily", b =>
                {
                    b.HasOne("invest.Model.Item", null)
                        .WithMany("Dailies")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("invest.Model.Point", b =>
                {
                    b.HasOne("invest.Model.Item", "Item")
                        .WithMany("Points")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("invest.Model.Item", b =>
                {
                    b.Navigation("Dailies");

                    b.Navigation("Points");
                });
#pragma warning restore 612, 618
        }
    }
}
