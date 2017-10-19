using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.IRepository
{
    public interface _IRepository<T> where T : class
    {
        
        void Add(T entity);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        void Delete(T entity);
        T Update(T entity);
        Task<bool> SaveChangesAsync();


    }
}
