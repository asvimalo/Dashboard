using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{

    public class Project 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }       
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        [Required]
        public string ProjectName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        [Required]
        public int TimeBudget { get; set; }
        public string Notes { get; set; }
        public ICollection<Assignment> Assignments { get; set; }  
        public ICollection<Phase> Phases { get; set; }
    }
}
