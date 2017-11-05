using Dashboard.DataG.EF.Contracts;
using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.DataG.EF.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Dashboard.DataG.EF.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class
    {
        private DashboardGenericContext _ctx;

        public GenericRepository(DashboardGenericContext ctx)
        {
            _ctx = ctx;
        }
        public async System.Threading.Tasks.Task<int> Create(TEntity entity)
        {
            await _ctx.Set<TEntity>().AddAsync(entity);
            
            return await _ctx.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var entity = await GetById(id);
            _ctx.Set<TEntity>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {

            return  _ctx.Set<TEntity>();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _ctx.Set<TEntity>()
                .FindAsync(id);
               

            //return null;
        }

        public async System.Threading.Tasks.Task<int> Update(int id, TEntity entity)
        {
            _ctx.Set<TEntity>().Update(entity);
            return await _ctx.SaveChangesAsync();


        }
        public IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeExpressions)
        {
            IQueryable<TEntity> dbQuery = _ctx.Set<TEntity>();

            //Apply eager loading
            foreach (Expression<Func<TEntity, object>> navigationProperty in includeExpressions)
                dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

            return dbQuery
                 .AsNoTracking();
                 
        }

    }
}
