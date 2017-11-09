using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.APIG.Models
{
    public class ProjectsEmployeesListNames
    {
        public IQueryable<Employee> Employees { get; set; }
        public IQueryable<Project> Projects { get; set; }
    }
}
