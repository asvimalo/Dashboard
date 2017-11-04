using Dashboard.DataG.EF.Contracts;
using Dashboard.DataG.EF.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using Dashboard.DataG.EF.Repository;

namespace Dashboard.Data.EF.Repository
{
    public class RepoTask : 
        GenericRepository<EntitiesG.EntitiesRev.Task>, 
        IRepoTask
    {
        private DashboardGenericContext _ctx;


        public RepoTask(DashboardGenericContext ctx): base (ctx)
        {
            _ctx = ctx;

        }

       
    }
}
