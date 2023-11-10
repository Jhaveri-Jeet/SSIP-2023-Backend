﻿// <auto-generated />
using CriminalDatabaseBackend.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231102072950_courtstableadded")]
    partial class courtstableadded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Acts.Acts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Acts");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Advocates.Advocates", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("EnrollmentNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Advocates");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.CaseType.CaseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("CaseTypes");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Courts.Courts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("FullAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("districtId")
                        .HasColumnType("int");

                    b.Property<int>("stateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("districtId");

                    b.HasIndex("stateId");

                    b.ToTable("Courts");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Districts.Districts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Roles.Roles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Sections.Sections", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ActId");

                    b.ToTable("Sections");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.States.States", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Users.Users", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Courts.Courts", b =>
                {
                    b.HasOne("CriminalDatabaseBackend.Features.Districts.Districts", "District")
                        .WithMany()
                        .HasForeignKey("districtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CriminalDatabaseBackend.Features.States.States", "State")
                        .WithMany()
                        .HasForeignKey("stateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");

                    b.Navigation("State");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Sections.Sections", b =>
                {
                    b.HasOne("CriminalDatabaseBackend.Features.Acts.Acts", "Act")
                        .WithMany()
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Act");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Users.Users", b =>
                {
                    b.HasOne("CriminalDatabaseBackend.Features.Roles.Roles", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
