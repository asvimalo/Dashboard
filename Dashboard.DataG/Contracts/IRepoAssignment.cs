using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.EF.Contracts
{
    public interface IRepoAssignment : IGenericRepository<Assignment>
    {
        Task<ICollection<Assignment>> GetProjectsByEmployeeId(int id);
    }
}
