using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;


namespace Dashboard.DataG.Repository
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

        
        

    }         
}
