using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{

    public class Project 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int ProjectId { get; set; }
       
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }

        [Required]
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        [Required]
        // public int TimeBudget { get; set; }
        public int Budget { get; set; } //Delete that
        public string Notes { get; set; }

        
        public Client Client { get; set; }
        //public ICollection<EmployeeProject> EmployeeProjects { get; set; } //DependOn
        public ICollection<Assignment> Assignments { get; set; }  //DependOn what name we choose
        public ICollection<Phase> Phases { get; set; }
    }
}
