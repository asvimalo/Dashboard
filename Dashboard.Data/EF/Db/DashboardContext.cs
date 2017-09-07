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
        

        public DashboardContext(DbContextOptions<DashboardContext> options)
               : base(options)
            {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Activities { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

                  
         }
    }
}
