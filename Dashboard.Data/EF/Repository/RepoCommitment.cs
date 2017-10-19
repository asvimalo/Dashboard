using Dashboard.Data.EF.Contracts;
using Dashboard.Data.EF.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EF.Repository
{
    public class RepoCommitment : Repository , IRepoCommitment
    {
        private DashboardContext _ctx;

        public RepoCommitment(DashboardContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
