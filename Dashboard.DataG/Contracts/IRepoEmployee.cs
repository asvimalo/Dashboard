using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.EF.Contracts
{
    public interface IRepoEmployee : IGenericRepository<Employee>
    {
        Task<Employee> GetSomething();
    }


}
