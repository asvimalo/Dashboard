using Dashboard.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.API.EF.Db
{
    public class DashboardContext : IdentityDbContext<User>
    {
        

        public DashboardContext(DbContextOptions options)
               : base(options)
            {
            
        }
        
        public DbSet<Project> Projects { get; set; }
        public DbSet<Picture> Pictures { get; set; }
        public DbSet<Commitment> Commitments { get; set; }

    }
}
