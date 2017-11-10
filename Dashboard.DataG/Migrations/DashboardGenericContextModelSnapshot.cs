﻿// <auto-generated />
using Dashboard.DataG.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Dashboard.DataG.Migrations
{
    [DbContext(typeof(DashboardGenericContext))]
    partial class DashboardGenericContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.AcquiredKnowledge", b =>
                {
                    b.Property<int>("AcquiredKnowledgeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<int>("KnowledgeId");

                    b.HasKey("AcquiredKnowledgeId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("KnowledgeId");

                    b.ToTable("EmployeeKnowledge");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EmployeeId");

                    b.Property<string>("Location");

                    b.Property<int>("ProjectId");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StopDate");

                    b.HasKey("AssignmentId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("EmployeeProject");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Client", b =>
                {
                    b.Property<int>("ClientId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientName");

                    b.Property<string>("Description");

                    b.Property<int>("LocationId");

                    b.HasKey("ClientId");

                    b.HasIndex("LocationId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Commitment", b =>
                {
                    b.Property<int>("CommitmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssigmentId");

                    b.Property<int>("Hours");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StopDate");

                    b.HasKey("CommitmentId");

                    b.HasIndex("AssigmentId");

                    b.ToTable("Commitment");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Employee", b =>
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

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.JobTitle", b =>
                {
                    b.Property<int>("JobTitleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("TitleName");

                    b.HasKey("JobTitleId");

                    b.ToTable("JobTitles");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.JobTitleAssignment", b =>
                {
                    b.Property<int>("JobTitleAssignmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AssignmentId");

                    b.Property<int>("JobTitleId");

                    b.HasKey("JobTitleAssignmentId");

                    b.HasIndex("AssignmentId");

                    b.HasIndex("JobTitleId");

                    b.ToTable("JobTitleAssignments");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Knowledge", b =>
                {
                    b.Property<int>("KnowledgeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("KnowledgeName");

                    b.HasKey("KnowledgeId");

                    b.ToTable("Knowledge");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Phase", b =>
                {
                    b.Property<int>("PhaseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("EndDate");

                    b.Property<string>("PhaseName");

                    b.Property<int>("Progress");

                    b.Property<int>("ProjectId");

                    b.Property<DateTime>("StartDate");

                    b.Property<int>("TimeBudget");

                    b.HasKey("PhaseId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Phase");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Project", b =>
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

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("PhaseId");

                    b.Property<string>("TaskName");

                    b.HasKey("TaskId");

                    b.HasIndex("PhaseId");

                    b.ToTable("Task");
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.AcquiredKnowledge", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Employee", "Employee")
                        .WithMany("AcquiredKnowledges")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Knowledge", "Knowledge")
                        .WithMany("AcquiredKnowledges")
                        .HasForeignKey("KnowledgeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Assignment", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Employee", "Employee")
                        .WithMany("Assignments")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Project", "Project")
                        .WithMany("Assignments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Client", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Location", "Location")
                        .WithMany("Clients")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Commitment", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Assignment", "Assignment")
                        .WithMany("Commitments")
                        .HasForeignKey("AssigmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.JobTitleAssignment", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Assignment", "Assignment")
                        .WithMany("JobTitleAssignments")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.JobTitle", "JobTitle")
                        .WithMany("JobTitleAssignments")
                        .HasForeignKey("JobTitleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Phase", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Project", "Project")
                        .WithMany("Phases")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Project", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Client", "Client")
                        .WithMany("Projects")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.EntitiesG.EntitiesRev.Task", b =>
                {
                    b.HasOne("Dashboard.EntitiesG.EntitiesRev.Phase", "Phase")
                        .WithMany("Tasks")
                        .HasForeignKey("PhaseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
