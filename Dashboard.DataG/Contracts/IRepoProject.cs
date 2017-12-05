using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.Contracts
{
    public interface IRepoProject : IGenericRepository<Project>
    {
        IList<Project> GetAllForReal();

        Task<IQueryable<Project>> GetProjectById(int id);

    }
}
