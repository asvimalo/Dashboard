﻿using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;


namespace Dashboard.DataG.Repository
{
    public class RepoPhase :
        GenericRepository<EntitiesG.EntitiesRev.Phase>,
        IRepoPhase
    {
        private DashboardGenericContext _ctx;


        public RepoPhase(DashboardGenericContext ctx) : base(ctx)
        {
            _ctx = ctx;

        }

       
    }
}
