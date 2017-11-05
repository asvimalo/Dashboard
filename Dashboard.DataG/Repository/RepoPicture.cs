using Dashboard.DataG.EF.Contracts;
using Dashboard.DataG.EF.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.EF.Repository
{
    public class RepoPicture :
        GenericRepository<EntitiesG.EntitiesRev.Picture>,
        IRepoPicture
    {
        private DashboardGenericContext _ctx;

        public RepoPicture(DashboardGenericContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        
    }
}
