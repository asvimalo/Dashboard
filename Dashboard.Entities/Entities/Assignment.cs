using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Entities
{
    [Table("Employee_Project")]
    public class Assignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignmentId { get; set; }
        
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
       
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public Project Project { get; set; }

        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public string Location { get; set; }

        public ICollection<Commitment> Commitments { get; set; }
       

        
    }
}