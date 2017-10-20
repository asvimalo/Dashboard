using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Contracts
{
    public interface IRepoAssignment : IRepo
    {
        Task<ICollection<Assignment>> GetProjectsByEmployeeId(int id);
    }
}
