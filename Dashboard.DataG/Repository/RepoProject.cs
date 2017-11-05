using Dashboard.DataG.EF.Contracts;
using Dashboard.DataG.EF.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.EF.Repository
{
    public class RepoProject :
        GenericRepository<EntitiesG.EntitiesRev.Project>,
        IRepoProject
    {
        private DashboardGenericContext _ctx;

        public RepoProject(DashboardGenericContext ctx) : base(ctx)
        { 
            _ctx = ctx;
        }
        

    }
}
