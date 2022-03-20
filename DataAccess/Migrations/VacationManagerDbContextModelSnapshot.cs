﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(VacationManagerDbContext))]
    partial class VacationManagerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Models.Project", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Models.Role", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Name = "CEO"
                        },
                        new
                        {
                            Name = "Developer"
                        },
                        new
                        {
                            Name = "Team Lead"
                        },
                        new
                        {
                            Name = "Unassigned"
                        });
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeamLeaderId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("TeamLeaderId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TeamId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleName");

                    b.HasIndex("TeamId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.Vacation", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApplicantId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsApproved")
                        .HasColumnType("bit");

                    b.Property<bool>("IsHalfDay")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("VacationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApplicantId");

                    b.ToTable("Vacations");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.HasOne("Models.Project", "Project")
                        .WithMany("Teams")
                        .HasForeignKey("ProjectId");

                    b.HasOne("Models.User", "TeamLeader")
                        .WithMany()
                        .HasForeignKey("TeamLeaderId");

                    b.Navigation("Project");

                    b.Navigation("TeamLeader");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.HasOne("Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleName");

                    b.HasOne("Models.Team", "Team")
                        .WithMany("Developers")
                        .HasForeignKey("TeamId");

                    b.Navigation("Role");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("Models.Vacation", b =>
                {
                    b.HasOne("Models.User", "Applicant")
                        .WithMany()
                        .HasForeignKey("ApplicantId");

                    b.Navigation("Applicant");
                });

            modelBuilder.Entity("Models.Project", b =>
                {
                    b.Navigation("Teams");
                });

            modelBuilder.Entity("Models.Team", b =>
                {
                    b.Navigation("Developers");
                });
#pragma warning restore 612, 618
        }
    }
}
