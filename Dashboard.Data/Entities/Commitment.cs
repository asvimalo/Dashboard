using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Commitment 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int CommitmentId { get; set; }
        //[ForeignKey("AssigmentId")]
        //public int AssigmentId { get; set; }
        

        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        //Rename Quantity to Hours
        public int Quantity { get; set; }

        //public Assigment Assignments { get; set; }

    }
}
