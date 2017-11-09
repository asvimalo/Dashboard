using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;


namespace Dashboard.DataG.Repository
{
    public class RepoLocation :
        GenericRepository<EntitiesG.EntitiesRev.Location>,
        IRepoLocation
    {
        private DashboardGenericContext _ctx;


        public RepoLocation(DashboardGenericContext ctx): base(ctx)
        {
            _ctx = ctx;

        }

        
    }
}
