using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.EntitiesG.EntitiesRev
{
    public class AssignmentPost
    {
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public string Location { get; set; }
        public ICollection<Commitment> Commitments { get; set; }
        public List<string> newJobTitles { get; set; }
        public ICollection<JobTitle> jobTitles { get; set; }
    }
}
