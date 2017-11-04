﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.EntitiesG.EntitiesRev
{
    [Table("Phase")]
    public class Phase : BaseEntity
    {
        
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }
        public string PhaseName { get; set; }
        public string Comments { get; set; }
       
        public ICollection<Task> Tasks { get; set; }
    }
}