﻿using Dashboard.DataG.EF.Contracts;
using Dashboard.DataG.EF.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.EF.Repository
{
    public class RepoKnowledge :
        GenericRepository<EntitiesG.EntitiesRev.Knowledge>,
        IRepoKnowledge
    {
        private DashboardGenericContext _ctx;


        public RepoKnowledge(DashboardGenericContext ctx) : base(ctx)
        {
            _ctx = ctx;

        }

        
    }
}
