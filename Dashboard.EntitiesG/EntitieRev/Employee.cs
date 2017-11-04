using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Dashboard.EntitiesG.EntitiesRev
{
    [Table("Employee")]
    public class Employee : BaseEntity
    {
        
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }    
        public string PersonNr { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public ICollection <AcquiredKnowledge> AcquiredKnowledges { get; set; } 
        public ICollection <Assignment> Assignments { get; set; }
    }
}