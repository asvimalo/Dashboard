using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.EF.Contracts
{
    public interface IRepoEmployee : IGenericRepository<Employee>
    {
        IQueryable<Employee> GetAllEmployees();
    }


}
