
using Dashboard.Data.EF.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.API.EF.IRepository;
using Dashboard.API.EF.Db;

namespace Dashboard.API.EF.Repository
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

        public async Task<T> Get(int id)
        {

            return await entities.SingleOrDefaultAsync(t => t.Id == id);

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            
            return await entities.ToListAsync();
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

