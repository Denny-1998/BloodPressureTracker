﻿// <auto-generated />
using System;
using MeasurementService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MeasurementService.Migrations
{
    [DbContext(typeof(MeasurementDbContext))]
    partial class MeasurementDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MeasurementService.Models.Measurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Diastolic")
                        .HasColumnType("int");

                    b.Property<string>("PatientSSN")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Seen")
                        .HasColumnType("bit");

                    b.Property<int>("Systolic")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PatientSSN");

                    b.ToTable("Measurements");
                });

            modelBuilder.Entity("MeasurementService.Models.Patient", b =>
                {
                    b.Property<string>("Ssn")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Ssn");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("MeasurementService.Models.Measurement", b =>
                {
                    b.HasOne("MeasurementService.Models.Patient", "Patient")
                        .WithMany("Measurements")
                        .HasForeignKey("PatientSSN")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MeasurementService.Models.Patient", b =>
                {
                    b.Navigation("Measurements");
                });
#pragma warning restore 612, 618
        }
    }
}
