using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;


namespace Dashboard.DataG.Repository
{
    public class RepoAcquiredKnowledge : 
        GenericRepository<EntitiesG.EntitiesRev.AcquiredKnowledge>, 
        IRepoAcquiredKnowledge
    {
       
        private DashboardGenericContext _ctx;

        public RepoAcquiredKnowledge(DashboardGenericContext ctx): base(ctx)
        {
            _ctx = ctx;
        }
        public IQueryable<AcquiredKnowledge> GetEmployeeKnowledge()
        {
            return _ctx.Set<AcquiredKnowledge>()
                .Include(e => e.Employee)
                .Include(k => k.Knowledge)
                .AsNoTracking();
        }

    }
}
