using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;

using System.Text;

namespace Dashboard.API.EF.IRepository
{
    interface IActivityRepo
    {
        ICollection<Project> GetActivities();
        void AddActivity(Project activity);
        Project UpdateActivity(Project activity);
        void DeleteActivity(int id);
    }
}
