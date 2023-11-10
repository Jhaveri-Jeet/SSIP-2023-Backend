﻿// <auto-generated />
using System;
using CriminalDatabaseBackend.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CriminalDatabaseBackend.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20231103141134_changeinthecourttableroleidadded")]
    partial class changeinthecourttableroleidadded
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

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Cases.Cases", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ActId")
                        .HasColumnType("int");

                    b.Property<int>("AdvocateId")
                        .HasColumnType("int");

                    b.Property<int>("AttorneyId")
                        .HasColumnType("int");

                    b.Property<string>("CaseStatus")
                        .HasColumnType("longtext");

                    b.Property<int>("CaseTypeId")
                        .HasColumnType("int");

                    b.Property<string>("CnrNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Comments")
                        .HasColumnType("longtext");

                    b.Property<int>("CourtId")
                        .HasColumnType("int");

                    b.Property<string>("DateFiled")
                        .HasColumnType("longtext");

                    b.Property<string>("Defendant")
                        .HasColumnType("longtext");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("JudgeName")
                        .HasColumnType("longtext");

                    b.Property<string>("Judgment")
                        .HasColumnType("longtext");

                    b.Property<string>("Petitioner")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ActId");

                    b.HasIndex("AdvocateId");

                    b.HasIndex("AttorneyId");

                    b.HasIndex("CaseTypeId");

                    b.HasIndex("CourtId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Courts.Courts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DistrictId")
                        .HasColumnType("int");

                    b.Property<string>("FullAddress")
                        .HasColumnType("longtext");

                    b.Property<string>("IdentificationNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int?>("RolesId")
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("RolesId");

                    b.HasIndex("StateId");

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

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Hearing.Hearing", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("HearingDate")
                        .HasColumnType("longtext");

                    b.Property<string>("HearingDetails")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Hearing");
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

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Witness.Witness", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("WitnessImage")
                        .HasColumnType("longtext");

                    b.Property<string>("WitnessName")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CaseId");

                    b.ToTable("Witness");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Cases.Cases", b =>
                {
                    b.HasOne("CriminalDatabaseBackend.Features.Acts.Acts", "Act")
                        .WithMany()
                        .HasForeignKey("ActId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CriminalDatabaseBackend.Features.Advocates.Advocates", "Advocate")
                        .WithMany()
                        .HasForeignKey("AdvocateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CriminalDatabaseBackend.Features.Advocates.Advocates", "Attorney")
                        .WithMany()
                        .HasForeignKey("AttorneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CriminalDatabaseBackend.Features.CaseType.CaseType", "CaseType")
                        .WithMany()
                        .HasForeignKey("CaseTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CriminalDatabaseBackend.Features.Courts.Courts", "Court")
                        .WithMany()
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Act");

                    b.Navigation("Advocate");

                    b.Navigation("Attorney");

                    b.Navigation("CaseType");

                    b.Navigation("Court");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Courts.Courts", b =>
                {
                    b.HasOne("CriminalDatabaseBackend.Features.Districts.Districts", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CriminalDatabaseBackend.Features.Roles.Roles", "Roles")
                        .WithMany()
                        .HasForeignKey("RolesId");

                    b.HasOne("CriminalDatabaseBackend.Features.States.States", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");

                    b.Navigation("Roles");

                    b.Navigation("State");
                });

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Hearing.Hearing", b =>
                {
                    b.HasOne("CriminalDatabaseBackend.Features.Cases.Cases", "Case")
                        .WithMany()
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
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

            modelBuilder.Entity("CriminalDatabaseBackend.Features.Witness.Witness", b =>
                {
                    b.HasOne("CriminalDatabaseBackend.Features.Cases.Cases", "Case")
                        .WithMany()
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });
#pragma warning restore 612, 618
        }
    }
}
