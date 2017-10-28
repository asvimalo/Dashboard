﻿// <auto-generated />
using Dashboard.Data.EF.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Dashboard.Data.Migrations
{
    [DbContext(typeof(DashboardContext))]
    partial class DashboardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dashboard.Entities.AcquiredKnowledge", b =>
                {
                    b.Property<int>("AcquiredKnowledgeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<int>("KnowledgeId");

                    b.HasKey("AcquiredKnowledgeId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("KnowledgeId");

                    b.ToTable("Knowledge_Employee");
                });

            modelBuilder.Entity("Dashboard.Entities.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<string>("JobTitle");

                    b.Property<string>("Location");

                    b.Property<int>("ProjectId");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StopDate");

                    b.HasKey("AssignmentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Employee_Project");
                });

            modelBuilder.Entity("Dashboard.Entities.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientName");

                    b.Property<string>("Description");

                    b.Property<int>("LocationId");

                    b.HasKey("ClientId");

                    b.HasIndex("LocationId");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Dashboard.Entities.Commitment", b =>
                {
                    b.Property<int>("CommitmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssigmentId");

                    b.Property<int>("Hours");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StopDate");

                    b.HasKey("CommitmentId");

                    b.HasIndex("AssigmentId");

                    b.ToTable("Commitments");
                });

            modelBuilder.Entity("Dashboard.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("ImageName");

                    b.Property<string>("ImagePath");

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PersonNr");

                    b.HasKey("EmployeeId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Dashboard.Entities.Knowledge", b =>
                {
                    b.Property<int>("KnowledgeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("KnowledgeName");

                    b.HasKey("KnowledgeId");

                    b.ToTable("Knowledges");
                });

            modelBuilder.Entity("Dashboard.Entities.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Dashboard.Entities.Phase", b =>
                {
                    b.Property<int>("PhaseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<string>("PhaseName");

                    b.Property<int>("ProjectId");

                    b.HasKey("PhaseId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Phases");
                });

            modelBuilder.Entity("Dashboard.Entities.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("EmployeeId");

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("PictureId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Dashboard.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<string>("Notes");

                    b.Property<string>("ProjectName")
                        .IsRequired();

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StopDate");

                    b.Property<int>("TimeBudget");

                    b.HasKey("ProjectId");

                    b.HasIndex("ClientId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Dashboard.Entities.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PhaseId");

                    b.Property<string>("TaskName");

                    b.HasKey("TaskId");

                    b.HasIndex("PhaseId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("Dashboard.Entities.AcquiredKnowledge", b =>
                {
                    b.HasOne("Dashboard.Entities.Employee", "Employee")
                        .WithMany("AcquiredKnowledge")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.Entities.Knowledge", "Knowledge")
                        .WithMany("AcquiredKnowledges")
                        .HasForeignKey("KnowledgeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Entities.Assignment", b =>
                {
                    b.HasOne("Dashboard.Entities.Employee", "Employee")
                        .WithMany("Assignments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.Entities.Project", "Project")
                        .WithMany("Assignments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Entities.Client", b =>
                {
                    b.HasOne("Dashboard.Entities.Location", "Location")
                        .WithMany("Clients")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Entities.Commitment", b =>
                {
                    b.HasOne("Dashboard.Entities.Assignment", "Assignment")
                        .WithMany("Commitments")
                        .HasForeignKey("AssigmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Entities.Phase", b =>
                {
                    b.HasOne("Dashboard.Entities.Project", "Project")
                        .WithMany("Phases")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Entities.Picture", b =>
                {
                    b.HasOne("Dashboard.Entities.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId");
                });

            modelBuilder.Entity("Dashboard.Entities.Project", b =>
                {
                    b.HasOne("Dashboard.Entities.Client", "Client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Entities.Task", b =>
                {
                    b.HasOne("Dashboard.Entities.Phase", "Phase")
                        .WithMany("Tasks")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
