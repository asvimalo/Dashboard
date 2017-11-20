using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dashboard.DataG.Contracts
{
    public interface IRepoProject : IGenericRepository<Project>
    {
        IList<Project> GetAllForReal();
    }
}
