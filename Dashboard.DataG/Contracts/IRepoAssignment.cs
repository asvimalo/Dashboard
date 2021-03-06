﻿using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.Contracts
{
    public interface IRepoAssignment : IGenericRepository<Assignment>
    {
        Task<IQueryable<Assignment>> GetProjectsByEmployeeId(int id);
        Task<IQueryable<Assignment>> GetAssignment(int id);
    }
}
