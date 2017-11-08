using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;
using Dashboard.EntitiesG.EntitiesRev;

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
    
    }
}
