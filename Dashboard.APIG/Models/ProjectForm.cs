using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.APIG.Models
{
    public class ProjectForm
    {
       
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public int TimeBudget { get; set; }
        public string Notes { get; set; }
        public int ClientId { get; set; }
        public List<int> Employees { get; set; }

    }
}
