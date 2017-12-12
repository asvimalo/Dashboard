
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dashboard.DataG.Db
{
    public class TempCtxDashboardGeneric
    {
        public class DashboardCtxFactory : IDesignTimeDbContextFactory<DashboardGenericContext>
        {
            public DashboardGenericContext CreateDbContext(string[] args)
            {
                var builder = new DbContextOptionsBuilder<DashboardGenericContext>();

                builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DashboardGenericDb;Trusted_Connection=True;MultipleActiveResultSets=true",
                    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(DashboardGenericContext).GetTypeInfo().Assembly.GetName().Name));
                // localdb connection: "Server=(localdb)\\mssqllocaldb;Database=DashboardGenericDb;Trusted_Connection=True;MultipleActiveResultSets=true"
                // SQL connection: "Server=(localdb)\\mssqllocaldb;Database=DashboardGenericDb;Trusted_Connection=True;MultipleActiveResultSets=true"
                return new DashboardGenericContext(builder.Options);
            }
        }
    }
}
