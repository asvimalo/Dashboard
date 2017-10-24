using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Commitment 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitmentId { get; set; }    
        public int AssigmentId { get; set; }
        [ForeignKey("AssigmentId")]
        public Assignment Assignment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public int Hours { get; set; }
    }
}
