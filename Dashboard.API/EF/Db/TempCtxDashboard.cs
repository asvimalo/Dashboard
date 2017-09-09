using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dashboard.API.EF.Db
{
    public class TempCtxDashboard
    {
        public class DashboardCtxFactory : IDesignTimeDbContextFactory<DashboardContext>
        {
            public DashboardContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<DashboardContext>();

                builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DashboardDb;Trusted_Connection=True;MultipleActiveResultSets=true",
                    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DashboardContext).GetTypeInfo().Assembly.GetName().Name));

                return new DashboardContext(builder.Options);
            }
        }
    }
}
