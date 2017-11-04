using Dashboard.DataG.EF.Contracts;
using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.DataG.EF.Db;
using Microsoft.EntityFrameworkCore;

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
        public async System.Threading.Tasks.Task Create(TEntity entity)
        {
            await _ctx.Set<TEntity>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var entity = await GetById(id);
            _ctx.Set<TEntity>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return  _ctx.Set<TEntity>().AsNoTracking();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await _ctx.Set<TEntity>()
                .FindAsync(id);
               

            //return null;
        }

        public async System.Threading.Tasks.Task Update(int id, TEntity entity)
        {
            _ctx.Set<TEntity>().Update(entity);
            await _ctx.SaveChangesAsync();


        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _ctx.SaveChangesAsync() > 0);
        }

    }
}
