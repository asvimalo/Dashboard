using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Contracts
{
    public interface IRepo
    {
        Task<T> AddAsync<T>(T entity) where T : class;

        void Delete<T>(T entity) where T : class;

        T Update<T>(T entity) where T : class;

        T Get<T>(int id) where T : class;

        Task<ICollection<T>> GetAll<T>() where T : class;

        Task<bool> SaveChangesAsync();
    }
}