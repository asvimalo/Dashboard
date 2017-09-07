using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dashboard.Data.Entities
{
    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }

        public ICollection<Commitment> Commitments { get; set; }
    }
}
