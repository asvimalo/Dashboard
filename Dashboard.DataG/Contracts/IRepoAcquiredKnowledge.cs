﻿using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard.DataG.EF.Contracts
{
    public interface IRepoAcquiredKnowledge : IGenericRepository<AcquiredKnowledge>
    {
        IQueryable<AcquiredKnowledge> GetEmployeeKnowledge();

    }
}
