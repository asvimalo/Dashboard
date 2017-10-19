using Dashboard.Data.EF.Db;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Dashboard.Data.EF.Contracts;

namespace Dashboard.Data.EF.Repository
{
    public class RepoAssignment : Repository, IRepoAssignment
    {
        private DashboardContext _ctx;

        public RepoAssignment(DashboardContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        public async Task<ICollection<Assignment>> GetProjectsByEmployeeId(int id)
        {
            var assigments = await _ctx.Assignments
                            .Include(e => e.Project)
                            .Include(c => c.Commitment)
                            .Where(p => p.EmployeeId == id)
                            .ToListAsync();
            return assigments;
        }

    }
}
