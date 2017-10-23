using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Dashboard.Data.Entities
{
    public class Employee 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }    
        public string PersonNr { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public int? PictureId { get; set; } //Delete
        [ForeignKey("PictureId")] //Delete
        public  Picture Picture { get; set; } //Delete
      
        //public ICollection <KnowledgeEmployee> KnowledgeEmployees { get; set; } ??AquiredKnowledge
        public ICollection <Assignment> Assignments { get; set; }
    }
}