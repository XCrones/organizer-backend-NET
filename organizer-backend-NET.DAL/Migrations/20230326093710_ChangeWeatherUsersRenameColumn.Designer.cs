﻿// <auto-generated />
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using organizer_backend_NET.DAL;
using organizer_backend_NET.Domain.Entity;

#nullable disable

namespace organizer_backend_NET.DAL.Migrations
{
    [DbContext(typeof(AppContextDb))]
    [Migration("20230326093710_ChangeWeatherUsersRenameColumn")]
    partial class ChangeWeatherUsersRenameColumn
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.Calendar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Background");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeleteAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("Description");

                    b.Property<DateTime>("EventEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("EventEnd");

                    b.Property<DateTime>("EventStart")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("EventStart");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.Property<int>("UId")
                        .HasColumnType("integer")
                        .HasColumnName("UId");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("UId");

                    b.ToTable("Calendars");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.Todo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Background")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("Background");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("Category");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime>("DeadLine")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeadLine");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeleteAt");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.Property<int>("Priority")
                        .HasColumnType("integer")
                        .HasColumnName("Priority");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean")
                        .HasColumnName("Status");

                    b.Property<int>("UId")
                        .HasColumnType("integer")
                        .HasColumnName("UId");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("UId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.User", b =>
                {
                    b.Property<int>("UId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UId"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeleteAt");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Password");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.Property<string>("UrlAvatar")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("UrlAvatar");

                    b.HasKey("UId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.WeatherForecast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeleteAt");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.Property<CityForecast>("city")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("city");

                    b.Property<int>("cnt")
                        .HasColumnType("integer")
                        .HasColumnName("cnt");

                    b.Property<List<WeatherItem>>("list")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("list");

                    b.HasKey("Id");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.WeatherUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("Id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<List<CityWeather>>("Cities")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("Cities");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime?>("DeleteAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DeleteAt");

                    b.Property<int>("UId")
                        .HasColumnType("integer")
                        .HasColumnName("UId");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("UId");

                    b.ToTable("WeatherUsers");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.Calendar", b =>
                {
                    b.HasOne("organizer_backend_NET.Domain.Entity.User", "User")
                        .WithMany("Calendar")
                        .HasForeignKey("UId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.Todo", b =>
                {
                    b.HasOne("organizer_backend_NET.Domain.Entity.User", "User")
                        .WithMany("Todo")
                        .HasForeignKey("UId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.WeatherUsers", b =>
                {
                    b.HasOne("organizer_backend_NET.Domain.Entity.User", "User")
                        .WithMany("WeatherUser")
                        .HasForeignKey("UId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("organizer_backend_NET.Domain.Entity.User", b =>
                {
                    b.Navigation("Calendar");

                    b.Navigation("Todo");

                    b.Navigation("WeatherUser");
                });
#pragma warning restore 612, 618
        }
    }
}
