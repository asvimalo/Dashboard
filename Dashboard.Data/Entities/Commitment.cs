using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Commitment 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitmentId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }
        public int Quantity { get; set; }
    }
}
