using Dashboard.Data.EF.Contracts;
using Dashboard.Data.EF.Db;
using Dashboard.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Repository
{
    public class RepoProject : Repository, IRepoProject
    {
        private DashboardContext _ctx;

        public RepoProject(DashboardContext ctx) : base(ctx)
        { 
            _ctx = ctx;
        }
        
        
    }
}
