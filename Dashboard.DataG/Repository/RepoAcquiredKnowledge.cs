using Dashboard.DataG.EF.Contracts;
using Dashboard.DataG.EF.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using System.Linq;


namespace Dashboard.DataG.EF.Repository
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

        
    }
}
