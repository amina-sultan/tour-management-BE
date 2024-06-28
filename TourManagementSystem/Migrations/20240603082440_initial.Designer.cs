﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TourManagementSystem.Data;

#nullable disable

namespace TourManagementSystem.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240603082440_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.0");

            modelBuilder.Entity("TourManagementSystem.Models.Destination", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BaseCost")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("DestinationName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal?>("HotelCosrPerDay")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Destinations");
                });
#pragma warning restore 612, 618
        }
    }
}