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
        public int AcquiredKnowledgeId { get; set; }    
        
        public int KnowledgeId { get; set; }
        [ForeignKey("KnowledgeId")]
        public Knowledge Knowledge { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
