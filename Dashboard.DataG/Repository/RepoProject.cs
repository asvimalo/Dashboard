using Dashboard.DataG.Contracts;
using Dashboard.DataG.Db;
using Dashboard.EntitiesG.EntitiesRev;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.DataG.Repository
{ 
    public class RepoProject :
        GenericRepository<EntitiesG.EntitiesRev.Project>,
        IRepoProject
    {
        private DashboardGenericContext _ctx;

        public RepoProject(DashboardGenericContext ctx) : base(ctx)
        { 
            _ctx = ctx;
        }
        public  IList<Project> GetAllForReal()
        {
            //var query = (from p in _ctx.Projects
            //             join a in _ctx.Assignments on p.ProjectId equals a.ProjectId
            //             join e in _ctx.Employees on a.EmployeeId equals e.EmployeeId
            //             join c in _ctx.Commitments on a.AssignmentId equals c.AssigmentId
            //             select  ).ToList();
            //return query;

            //string query = ($@"SELECT * FROM  Project as p
            //                left JOIN EmployeeProject as ep ON p.ProjectId = ep.ProjectId
            //                left JOIN Employee as e On ep.EmployeeId = e.EmployeeId
            //                left JOIN Commitment as c On ep.AssignmentId = c.AssigmentId");
            //var projects = _ctx.Projects
            //    .FromSql(query).ToList();

            //var projects = new List<Project>();
            //projects = _ctx.Projects.Where(x => _ctx.Employees.Any(y => y.EmployeeId == x.Employee)
            
            var projects = new List<Project>();
            projects = _ctx.Projects
                .Include(i => i.Phases)
                .Include(i => i.Assignments)
                .ToList();

            return projects;

        }

        public async Task<IQueryable<Project>> GetProjectById (int id)
        {
            var project = _ctx.Projects
                .Include(c => c.Client)
                .Include(i => i.Phases)
                .Include(a => a.Assignments).ThenInclude(a => a.Employee)
                .Include(a => a.Assignments)
                .ThenInclude(a => a.JobTitleAssignments)
                .ThenInclude(a => a.JobTitle)
                .Include(a => a.Assignments)
                .ThenInclude(a => a.Commitments)
                .Where(p => p.ProjectId == id);

            return project;
        }
    }
}
