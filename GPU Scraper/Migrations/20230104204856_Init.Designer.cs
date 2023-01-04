﻿// <auto-generated />
using System;
using GPU_Scraper.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GPUScraper.Migrations
{
    [DbContext(typeof(GPUScraperDbContext))]
    [Migration("20230104204856_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GPU_Scraper.Entities.GPU", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("HighestPrice")
                        .HasPrecision(7, 2)
                        .HasColumnType("float(7)");

                    b.Property<string>("HighestPriceShop")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("LowestPrice")
                        .HasPrecision(7, 2)
                        .HasColumnType("float(7)");

                    b.Property<string>("LowestPriceShop")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GPUs");
                });
#pragma warning restore 612, 618
        }
    }
}
