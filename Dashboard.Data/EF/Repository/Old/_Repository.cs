using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Dashboard.Data.EF.IRepository;
using Dashboard.Data.EF.Db;
using Dashboard.Entities;

namespace Dashboard.Data.EF.Repository
{
    public class _Repository 
    {
        private DashboardContext _ctx;
        

        public _Repository(DashboardContext ctx)
        {
            _ctx = ctx;
            
        }



        #region CRUD without gets
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
        public async Task<bool> SaveChangesAsync()
        {
            return (await _ctx.SaveChangesAsync() > 0);
        } 
        #endregion



        //#region GetById
        //public async Task<Project> GetProject(int id)
        //{
        //    return await _ctx.Projects                                                       
        //                    .Include(c => c.Commitments)
        //                    .FirstOrDefaultAsync(t => t.ProjectId == id);
        //}
        //public async Task<Commitment> GetCommitment(int id)
        //{
        //    return await _ctx.Commitments
        //                    .Include(x => x.Project)
        //                    .Include(y => y.User)
        //                    .Where(com => com.CommitmentId == id)
        //                    .FirstOrDefaultAsync();
        //}
        //public async Task<Employee> GetUser(int id)
        //{
        //    return await _ctx.Users
        //                    .Include(c => c.Picture)
        //                    .Where(t => t.UserId == id)
        //                    .FirstOrDefaultAsync();
        //}
        //public async Task<Picture> GetPicture(int id)
        //{
        //    return await _ctx.Pictures
        //                    .Include(c => c.User)
        //                    .Where(t => t.PictureId == id)
        //                    .FirstOrDefaultAsync();
        //}
        //#endregion

        //#region GetAll
        //public async Task<ICollection<Project>> GetProjects()
        //{

        //    return await _ctx.Projects
        //                    .Include(c => c.Commitments)
        //                        .ThenInclude((x => x.User))
        //                            .ThenInclude(p => p.Picture)
        //                    .ToListAsync();
        //}
        //public async Task<ICollection<Commitment>> GetCommitments()
        //{
        //    return await _ctx.Commitments
        //                    .Include(x => x.Project)
        //                    .Include(y => y.User)
        //                        .ThenInclude(p => p.Picture)
        //                    .ToListAsync();
        //}
        //public async Task<ICollection<Employee>> GetUsers()
        //{

        //    return await _ctx.Users
        //                    .Include(p => p.Picture)
        //                    .Include(cc => cc.Commitments)
        //                    .ToListAsync();
        //}
        //public async Task<ICollection<Picture>> GetPictures() => await _ctx.Pictures
        //                    .Include(p => p.User)
        //                    .ToListAsync();

        //#endregion

        //#region Commitments by project/User
        //public async Task<ICollection<Commitment>> GetCommitmentsByProjectId(int id) => await _ctx.Commitments
        //                    .Include(x => x.Project)
        //                    .Include(y => y.User)
        //                        .ThenInclude(p => p.Picture)
        //                    .Where(project => project.ProjectId == id)
        //                    .ToListAsync();
        //public async Task<ICollection<Commitment>> GetCommitmentsByUserId(int id)
        //{
        //    return await _ctx.Commitments
        //                    .Include(x => x.Project)
        //                    .Include(y => y.User)
        //                        .ThenInclude(p => p.Picture)
        //                    .Where(user => user.UserId == id)
        //                    .ToListAsync();
        //}
        //#endregion

        //#region User projects
        //public async Task<ICollection<Employee>> GetUsersByProjectId(int id)
        //{

        //    // TODO TEST
        //    var users = await _ctx.Users
        //                 .Include(pic => pic.Picture)
        //                 .Include(com => com.Commitments)
        //                    .ThenInclude(p => p.ProjectId == id)
        //                 .ToListAsync(); 
                         

        //    return users;
                            
        //}
        //// TODO TEST
        //public async Task<ICollection<Project>> GetProjectsByUserId(int id)
        //{
        //    var projects = await _ctx.Projects
        //                    .Include(x => x.Commitments)
        //                        .ThenInclude(user => user.UserId == id)
        //                    .ToListAsync();
        //    return projects;
        //}
        //#endregion
    }

}

