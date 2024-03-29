﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NGK_LAB10_WebAPI.Data;

namespace NGK_LAB10_WebAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NGK_LAB10_WebAPI.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("NGK_LAB10_WebAPI.Models.WeatherObservation", b =>
                {
                    b.Property<int>("WeatherObservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AirPressure")
                        .HasColumnType("decimal(10,1)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Humidity")
                        .HasColumnType("int");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.Property<decimal>("TemperatureC")
                        .HasColumnType("decimal(10,1)");

                    b.HasKey("WeatherObservationId");

                    b.HasIndex("LocationId");

                    b.ToTable("WeatherObservation");
                });

            modelBuilder.Entity("NGK_LAB10_WebAPI.Models.WeatherStationClient", b =>
                {
                    b.Property<long>("WeatherStationClientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PwHash")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.HasKey("WeatherStationClientId");

                    b.ToTable("WeatherStationClient");
                });

            modelBuilder.Entity("NGK_LAB10_WebAPI.Models.WeatherObservation", b =>
                {
                    b.HasOne("NGK_LAB10_WebAPI.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");
                });
#pragma warning restore 612, 618
        }
    }
}
