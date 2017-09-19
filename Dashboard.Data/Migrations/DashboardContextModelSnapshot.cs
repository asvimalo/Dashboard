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

            modelBuilder.Entity("Dashboard.Data.Entities.Commitment", b =>
                {
                    b.Property<int>("CommitmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("ProjectId");

                    b.Property<int>("UserId");

                    b.HasKey("CommitmentId");

                    b.HasIndex("UserId");

                    b.ToTable("Commitments");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Picture", b =>
                {
                    b.Property<int>("PictureId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.Property<int?>("UserId");

                    b.HasKey("PictureId");

                    b.HasIndex("UserId");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<DateTime>("StartDate");

                    b.Property<DateTime>("StopDate");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<string>("PersonNr");

                    b.Property<int?>("PictureId");

                    b.HasKey("UserId");

                    b.HasIndex("PictureId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Commitment", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Project", "Project")
                        .WithMany("Commitments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dashboard.Data.Entities.User", "User")
                        .WithMany("Commitments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dashboard.Data.Entities.Picture", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Dashboard.Data.Entities.User", b =>
                {
                    b.HasOne("Dashboard.Data.Entities.Picture", "Picture")
                        .WithMany()
                        .HasForeignKey("PictureId");
                });
#pragma warning restore 612, 618
        }
    }
}
