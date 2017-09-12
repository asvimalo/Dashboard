using Dashboard.Data.EF.Entities;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dashboard.Data.EF.IRepository;
using Dashboard.Data.EF.Db;
using Dashboard.Data.Entities;

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

        public async Task<T> Get(int id)
        {
            var entity = entities.Find(id);
            if (entity is Project)
                return await _ctx.Projects.SingleOrDefaultAsync(t => t.ProjectId == id) as T;
            else if (entity is Commitment)
                return await _ctx.Commitments.SingleOrDefaultAsync(t => t.CommitmentId == id) as T;
            else if (entity is User)
                return await _ctx.Users.SingleOrDefaultAsync(t => t.UserId == id) as T;
            else if (entity is Picture)
                return await _ctx.Pictures.SingleOrDefaultAsync(t => t.PictureId == id) as T;
            else
                return null;

        }

        public async Task<IEnumerable<T>> GetAll()
        {
            //var type = entities.GetType() as DbSet<T>;

            //if (type is Project)
            //    return await _ctx.Projects.Include(x => x.Commitments).ToListAsync() as IEnumerable<T>;
            //else if (type is Commitment)
            //    return await _ctx.Commitments.Include(x => x.User).Include(x => x.Project).ToListAsync() as IEnumerable<T>;
            //else if (type is User)
            //    return await _ctx.Users.Include(x => x.Commitments).ToListAsync() as IEnumerable<T>;
            //else if (type is Picture)
            //    return await _ctx.Pictures.ToListAsync() as IEnumerable<T>;
            //else
            //    return null;
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

