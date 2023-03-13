﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using måleusikkerhet.Database;

#nullable disable

namespace måleusikkerhet.Migrations
{
    [DbContext(typeof(DeviceDb))]
    [Migration("20230313140845_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("måleusikkerhet.Bases.Analog", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("AnalogDev");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Digital", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<double?>("Resolution")
                        .HasColumnType("double precision");

                    b.HasKey("Name");

                    b.ToTable("DigitalDev");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Precise", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("PreciseDev");
                });

            modelBuilder.Entity("måleusikkerhet.Infrastructure.DigitalAttributes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("DigitalName")
                        .HasColumnType("text");

                    b.Property<double>("Digits")
                        .HasColumnType("double precision");

                    b.Property<double>("Range")
                        .HasColumnType("double precision");

                    b.Property<double>("RangeError")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("DigitalName");

                    b.ToTable("DigitalAttributes");
                });

            modelBuilder.Entity("måleusikkerhet.Infrastructure.DigitalAttributes", b =>
                {
                    b.HasOne("måleusikkerhet.Bases.Digital", null)
                        .WithMany("Ranges")
                        .HasForeignKey("DigitalName");
                });

            modelBuilder.Entity("måleusikkerhet.Bases.Digital", b =>
                {
                    b.Navigation("Ranges");
                });
#pragma warning restore 612, 618
        }
    }
}
