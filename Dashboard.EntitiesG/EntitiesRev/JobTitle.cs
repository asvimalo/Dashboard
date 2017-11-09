
using System.Collections.Generic;

namespace Dashboard.EntitiesG.EntitiesRev
{
    public class JobTitle
    {
        public int JobTitleId { get; set; }
        public string TitleName { get; set; }
        public ICollection<JobTitleAssignment> JobTitleAssignments { get; set; }
    }
}