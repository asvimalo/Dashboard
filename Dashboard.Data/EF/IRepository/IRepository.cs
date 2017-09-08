using Dashboard.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.EF.IRepository
{
    public interface IRepository<T> where T : BaseEntity
    {
        
        void Add(T entity);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Delete(T entity);
        T Update(T entity);
        
        
    }
}
