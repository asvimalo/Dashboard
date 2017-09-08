using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Dashboard.Data.EF.Db
{
    public class DashboardCtxFactory : IDesignTimeDbContextFactory<DashboardContext>
    {
        //public DashboardContext Create(DbContextFactoryOptions options)
        //{
           
        //        var builder = new DbContextOptionsBuilder<DashboardContext>();
        //        builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DashboardDb;Trusted_Connection=True;MultipleActiveResultSets=true",
        //            optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DashboardContext).GetTypeInfo().Assembly.GetName().Name));
        //        return new DashboardContext(builder.Options);
            
        //}

        public DashboardContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DashboardContext>();
            builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DashboardDb;Trusted_Connection=True;MultipleActiveResultSets=true",
                optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DashboardContext).GetTypeInfo().Assembly.GetName().Name));
            return new DashboardContext(builder.Options);
        }
    }
}
