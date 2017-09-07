using Dashboard.Data.EF.IRepository;
using Dashboard.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Dashboard.Data.EF.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private DashboardContext _ctx;
        private DbSet<T> entities;

        public Repository(DashboardContext ctx)
        {
            _ctx = ctx;
            entities = _ctx.Set<T>();
        }
        public void Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            //_ctx.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            //_ctx.SaveChanges();
        }

        public T Get(int id)
        {

            return null;// entities.SingleOrDefault(t => t.Id == id);

        }

        public ICollection<T> GetAll()
        {
            return entities.ToList();
        }

        public T Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _ctx.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _ctx.SaveChangesAsync() > 0);
        }

        
    }
    
}

