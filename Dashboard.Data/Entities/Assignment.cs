using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    [Table("Employee_Project")]
    public class Assignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int AssignmentId { get; set; }

        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        
        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        //public string Location { get; set; }


        public Employee Employee { get; set; }
        public Project Project { get; set; }

        //public ICollection<Commitment> Commitments { get; set; }
        public int CommitmentId { get; set; } // Wrong!!! ONE to many side.. Delete Foreign key is on the other side
        [ForeignKey("CommitmentId")] //Delete
        public Commitment Commitment { get; set; } //Delete

        
    }
}