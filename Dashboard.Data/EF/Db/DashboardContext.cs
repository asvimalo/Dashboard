using Dashboard.Data.Entities;
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
        public DbSet<User> Users { get; set; }
        public DbSet<Commitment> Commitments { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Picture>().ToTable("Picture");
            modelBuilder.Entity<Project>().ToTable("Project");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Commitment>().ToTable("Commitment");
           
                
        }
    }
}
