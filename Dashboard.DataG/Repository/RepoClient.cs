using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.Repository
{
    public class RepoClient : 
        GenericRepository<EntitiesG.EntitiesRev.Client>, 
        IRepoClient
    {
        private DashboardGenericContext _ctx;


        public RepoClient(DashboardGenericContext ctx): base(ctx)
        {
            _ctx = ctx;

        }
        
    }
}
