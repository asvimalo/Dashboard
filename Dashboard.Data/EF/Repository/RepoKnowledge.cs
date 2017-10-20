using Dashboard.Data.EF.Contracts;
using Dashboard.Data.EF.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Repository
{
    public class RepoKnowledge : IRepoKnowledge
    {
        private DashboardContext _ctx;


        public RepoKnowledge(DashboardContext ctx)
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
        }

        public void Delete<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _ctx.Remove(entity);
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

        public T Update<T>(T entity) where T : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            _ctx.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}
