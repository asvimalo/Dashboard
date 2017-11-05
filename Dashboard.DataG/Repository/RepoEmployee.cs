using Dashboard.DataG.EF.Contracts;
using Dashboard.DataG.EF.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dashboard.EntitiesG.EntitiesRev;
using System.Linq;

namespace Dashboard.DataG.EF.Repository
{
    public class RepoEmployee :
        GenericRepository<EntitiesG.EntitiesRev.Employee>,
        IRepoEmployee
    {
        private DashboardGenericContext _ctx;
        public RepoEmployee(DashboardGenericContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }

        
    }         
}
