﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using måleusikkerhet.Database;

#nullable disable

namespace måleusikkerhet.Migrations
{
    [DbContext(typeof(DeviceDb))]
    partial class DeviceDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("måleusikkerhet.Bases.Analog", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeasumentType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RelativeRange")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.HasIndex("ImageId");

                    b.ToTable("AnalogDev");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Digital", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeasumentType")
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Resolution")
                        .HasColumnType("REAL");

                    b.HasKey("Name");

                    b.HasIndex("ImageId");

                    b.ToTable("DigitalDev");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Precise", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ImageId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MeasumentType")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.HasIndex("ImageId");

                    b.ToTable("PreciseDev");
                });

            modelBuilder.Entity("måleusikkerhet.Database.AnalogAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AnalogName")
                        .HasColumnType("TEXT");

                    b.Property<double>("Precision")
                        .HasColumnType("REAL");

                    b.Property<double>("Range")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("AnalogName");

                    b.ToTable("AnalogAttributes");
                });

            modelBuilder.Entity("måleusikkerhet.Database.DigitalAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("DigitalName")
                        .HasColumnType("TEXT");

                    b.Property<double>("Digits")
                        .HasColumnType("REAL");

                    b.Property<double?>("Frequency")
                        .HasColumnType("REAL");

                    b.Property<int>("MeasurementType")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Range")
                        .HasColumnType("REAL");

                    b.Property<double>("RangeError")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("DigitalName");

                    b.ToTable("DigitalAttributes");
                });

            modelBuilder.Entity("måleusikkerhet.Database.ImageDb", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.ToTable("ImageDb");
                });

            modelBuilder.Entity("måleusikkerhet.Database.PreciseAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double?>("Frequency")
                        .HasColumnType("REAL");

                    b.Property<double>("MeasureError")
                        .HasColumnType("REAL");

                    b.Property<int>("MeasurementType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PreciseName")
                        .HasColumnType("TEXT");

                    b.Property<double>("Range")
                        .HasColumnType("REAL");

                    b.Property<double>("RangeError")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("PreciseName");

                    b.ToTable("PreciseAttributes");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Analog", b =>
                {
                    b.HasOne("måleusikkerhet.Database.ImageDb", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Digital", b =>
                {
                    b.HasOne("måleusikkerhet.Database.ImageDb", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Precise", b =>
                {
                    b.HasOne("måleusikkerhet.Database.ImageDb", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId");

                    b.Navigation("Image");
                });

            modelBuilder.Entity("måleusikkerhet.Database.AnalogAttributes", b =>
                {
                    b.HasOne("måleusikkerhet.Bases.Analog", null)
                        .WithMany("Ranges")
                        .HasForeignKey("AnalogName");
                });

            modelBuilder.Entity("måleusikkerhet.Database.DigitalAttributes", b =>
                {
                    b.HasOne("måleusikkerhet.Bases.Digital", null)
                        .WithMany("Ranges")
                        .HasForeignKey("DigitalName");
                });

            modelBuilder.Entity("måleusikkerhet.Database.PreciseAttributes", b =>
                {
                    b.HasOne("måleusikkerhet.Bases.Precise", null)
                        .WithMany("Ranges")
                        .HasForeignKey("PreciseName");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Analog", b =>
                {
                    b.Navigation("Ranges");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Digital", b =>
                {
                    b.Navigation("Ranges");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Precise", b =>
                {
                    b.Navigation("Ranges");
                });
#pragma warning restore 612, 618
        }
    }
}
