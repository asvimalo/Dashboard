using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard.DataG.Repository
{
    public class RepoJobTitleAssignment :
        GenericRepository<JobTitleAssignment>,
        IRepoJobTitleAssignment 
    {
        private DashboardGenericContext _ctx;

        public RepoJobTitleAssignment(DashboardGenericContext ctx): base(ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<JobTitleAssignment> getAllOfThem()
        {
            return _ctx.JobTitleAssignments.Include(x => x.Assignment).Include(y => y.JobTitle);
        }
    }
}
