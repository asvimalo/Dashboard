
using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard.DataG.Contracts
{
    public interface IRepoJobTitleAssignment :
         IGenericRepository<JobTitleAssignment>
    {
        IQueryable<JobTitleAssignment> getAllOfThem();
    }
}
