using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dashboard.Data.Entities
{
    [Table("Knowledge_Employee")]
    public class AcquiredKnowledge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key] using missing
        public int AcquiredKnowledgeId { get; set; }     
        
        [ForeignKey("KnowledgeId")]
        public int KnowledgeId { get; set; }
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }

        public Knowledge Knowledge { get; set; }
        public Employee Employee { get; set; }
    }
}
