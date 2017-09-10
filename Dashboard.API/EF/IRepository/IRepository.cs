using Dashboard.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.API.EF.IRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        
        void Add(T entity);
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll();
        void Delete(T entity);
        T Update(T entity);
        Task<bool> SaveChangesAsync();


    }
}
