using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;

using System.Text;

namespace Dashboard.Data.EF.IRepository
{
    interface IActivityRepo
    {
        ICollection<Project> GetActivities();
        void AddActivity(Project activity);
        Project UpdateActivity(Project activity);
        void DeleteActivity(int id);
    }
}
