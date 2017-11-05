using Dashboard.DataG.EF.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dashboard.DataG.EF.Contracts;


namespace Dashboard.DataG.EF.Repository
{
    public class RepoAssignment : 
        GenericRepository<EntitiesG.EntitiesRev.Assignment>, 
        IRepoAssignment
    {
        private DashboardGenericContext _ctx;

        public RepoAssignment(DashboardGenericContext ctx):base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<Assignment>> GetProjectsByEmployeeId(int id)
        {
            return  _ctx.Assignments
                .Include(a => a.Commitments)
                .Include(b => b.Location)
                    .Include(x => x.Project)
                    .ThenInclude(c => c.Phases)
                .Where(p => p.EmployeeId == id).AsNoTracking();


        }
    }
}
