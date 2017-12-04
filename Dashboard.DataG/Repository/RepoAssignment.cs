using Dashboard.DataG.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dashboard.DataG.Contracts;


namespace Dashboard.DataG.Repository
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
            return _ctx.Assignments
                .Include(a => a.Commitments)
                .Include(j => j.JobTitleAssignments).ThenInclude(j => j.JobTitle)
                .Include(p => p.Project)
                .Include(x => x.Employee)
                .Where(e => e.EmployeeId == id || e.AssignmentId == id).AsNoTracking();

         }

        public async Task<IQueryable<Assignment>> GetAssignment(int id)
        {
            return _ctx.Assignments
                .Include(a => a.Commitments)
                .Include(j => j.JobTitleAssignments).ThenInclude(j => j.JobTitle)
                .Include(p => p.Project)
                .Include(x => x.Employee)
                .Where(e => e.AssignmentId == id).AsNoTracking();

        }
    }
}
