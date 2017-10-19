using Dashboard.Data.EF.Contracts;
using Dashboard.Data.EF.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EF.Repository
{
    public class RepoEmployee : Repository, IRepoEmployee
    {
        private DashboardContext _ctx;
        public RepoEmployee(DashboardContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
