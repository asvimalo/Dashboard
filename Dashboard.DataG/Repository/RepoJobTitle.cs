using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Dashboard.DataG.Repository
{
    public class RepoJobTitle :
        GenericRepository<JobTitle>,
        IRepoJobTitle
    {
        private DashboardGenericContext _ctx;
        public RepoJobTitle(DashboardGenericContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<JobTitle> getAllOfThem()
        {
            return _ctx.JobTitles.Include(x => x.JobTitleAssignments);
        }

    }
}
