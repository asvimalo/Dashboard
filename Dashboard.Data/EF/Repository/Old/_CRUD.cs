using Dashboard.Data.EF.Db;
using Dashboard.Data.EF.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Repository
{
    public class _CRUD<T > : _ICRUD<T> where T : class
    {
        private DashboardContext _ctx;


        public _CRUD(DashboardContext ctx)
        {
            _ctx = ctx;
            var Entity = _ctx.Set<T>();

        }

        public async Task<T> AddAsync(T entity) 
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var addedEntity = await _ctx.AddAsync(entity);
            return addedEntity.Entity;
            //_ctx.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _ctx.Remove(entity);
            //_ctx.SaveChanges();
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
        public T Get(int id)
        {
            return _ctx.Set<T>().Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _ctx.Set<T>();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _ctx.SaveChangesAsync() > 0);
        }
    
    }
}