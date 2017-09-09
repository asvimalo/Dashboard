using Dashboard.API.EF.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dashboard.Data.Entities;

namespace Dashboard.API.EF.Repository
{
    public class ActivityRepo : IActivityRepo
    {
        public void AddActivity(Project activity)
        {
            throw new NotImplementedException();
        }

        public void DeleteActivity(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Project> GetActivities()
        {
            throw new NotImplementedException();
        }

        public Project UpdateActivity(Project activity)
        {
            throw new NotImplementedException();
        }
    }
}
