using Dashboard.Data.EF.Contracts;
using Dashboard.Data.EF.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Data.EF.Repository
{
    public class RepoAcquiredKnowledge : IRepoAcquiredKnowledge
    {
        private DashboardContext _ctx;


        public RepoAcquiredKnowledge(DashboardContext ctx)
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

        public Task<ICollection<T>> GetAll<T>() where T : class
        {
            throw new NotImplementedException();
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
