using Dashboard.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Db
{
    public class DashboardContext : DbContext
    {


        public DashboardContext(DbContextOptions options)
               : base(options)
        {

        }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Commitment> Commitments { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Knowledge> Knowledges { get; set; }
        public DbSet<AcquiredKnowledge> AcquiredKnowledges { get; set; }
        public DbSet<Phase> Phases { get; set; }
        public DbSet<Dashboard.Entities.Task> Tasks { get; set; }
        public DbSet<Location> Location { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region modelBuilder
            ////modelBuilder.Entity<Picture>()
            ////   .HasKey(p => p.PictureId);

            ////modelBuilder.Entity<Picture>().ToTable("Picture");

            ////modelBuilder.Entity<Project>()
            ////    .HasKey(p => p.ProjectId);
            ////modelBuilder.Entity<Project>()
            ////    .HasMany(p => p.Commitments)
            ////    .WithOne();
            ////modelBuilder.Entity<Project>().ToTable("Project");



            ////modelBuilder.Entity<User>()
            ////    .HasKey(k => k.UserId);
            ////modelBuilder.Entity<User>()
            ////    .HasOne(k => k.Picture)
            ////    .WithOne(s => s.User)
            ////    .HasForeignKey<Picture>(kt => kt.Picture);
            ////modelBuilder.Entity<User>()
            ////    .HasMany(k => k.Commitments)
            ////    .WithOne();
            ////modelBuilder.Entity<User>().ToTable("User");

            ////modelBuilder.Entity<Commitment>()
            ////    .HasKey(k => k.CommitmentId);
            ////modelBuilder.Entity<Commitment>()
            ////    .HasOne(k => k.User)
            ////    .WithMany(t => t.Commitments)
            ////    .HasForeignKey(kt => kt.UserId);
            ////modelBuilder.Entity<Commitment>()
            ////    .HasOne(k => k.Project)
            ////    .WithMany(t => t.Commitments)
            ////    .HasForeignKey(kt => kt.ProjectId);
            ////modelBuilder.Entity<Commitment>().ToTable("Commitment"); 
            #endregion



        }
    }
}
