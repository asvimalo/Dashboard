using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;


namespace Dashboard.DataG.Repository
{
    public class RepoPicture :
        GenericRepository<EntitiesG.EntitiesRev.Picture>,
        IRepoPicture
    {
        private DashboardGenericContext _ctx;

        public RepoPicture(DashboardGenericContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        
    }
}
