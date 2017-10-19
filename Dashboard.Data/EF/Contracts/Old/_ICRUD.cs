using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.IRepository
{
    public interface _ICRUD<T> where T : class
    {
        Task<T> AddAsync(T entity);
        void Delete(T entity);
        T Update(T entity);
        T Get(int id);
        IQueryable<T> GetAll();
        Task<bool> SaveChangesAsync();
    }
}
