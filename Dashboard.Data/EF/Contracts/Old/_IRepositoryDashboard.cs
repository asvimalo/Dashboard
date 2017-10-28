using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard.Entities;

namespace Dashboard.Data.EF.IRepository
{
    public interface _IRepositoryDashboard
    {
        Task<T> AddAsync<T>(T entity) where T : class;
        T Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<Commitment> GetCommitment(int id);
        Task<ICollection<Commitment>> GetCommitments();
        Task<ICollection<Commitment>> GetCommitmentsByProjectId(int id);
        Task<ICollection<Commitment>> GetCommitmentsByUserId(int id);
        Task<Picture> GetPicture(int id);
        Task<ICollection<Picture>> GetPictures();
        Task<Project> GetProject(int id);
        Task<ICollection<Project>> GetProjects();
        Task<ICollection<Project>> GetProjectsByUserId(int id);
        Task<Employee> GetUser(int id);
        Task<ICollection<Employee>> GetUsers();
        Task<ICollection<Employee>> GetUsersByProjectId(int id);
        Task<bool> SaveChangesAsync();
        
    }
}