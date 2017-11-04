using Dashboard.Data.EF.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dashboard.Data.EF;
using Dashboard.Data.EF.Contracts;

namespace Dashboard.Data.EF.Repository
{
    public class Repository : IRepo
    {
        private DashboardContext _ctx;


        public Repository(DashboardContext ctx)
        {
            _ctx = ctx;

        }

        public async Task<T> AddAsync<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var addedEntity = await _ctx.AddAsync(entity);
            return addedEntity.Entity;
            //_ctx.SaveChanges();
        }
        public void Delete<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _ctx.Remove(entity);
            //_ctx.SaveChanges();
        }
        public T Update<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _ctx.Entry(entity).State = EntityState.Modified;
            return entity;
        }
        public T Get<T>(int id) where T : class
        {
            return _ctx.Set<T>().Find(id);
        }

        public async Task<ICollection<T>> GetAll<T>() where T : class
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _ctx.SaveChangesAsync() > 0);
        }
       


    }
}