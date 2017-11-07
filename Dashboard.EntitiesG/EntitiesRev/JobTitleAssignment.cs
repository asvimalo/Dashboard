
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dashboard.EntitiesG.EntitiesRev
{
    public class JobTitleAssignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int JobTitleAssignmentId { get; set; }
        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
        public int JobTitleId { get; set; }
        public JobTitle JobTitle { get; set; }
        
    }
}
