﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WeatherForecastAPI.Receiver.Data;

#nullable disable

namespace WeatherForecastAPI.Receiver.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241022135532_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WeatherForecastAPI.Receiver.Models.Weather", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Summary")
                        .HasColumnType("text");

                    b.Property<int>("TemperatureC")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Weathers");
                });
#pragma warning restore 612, 618
        }
    }
}
