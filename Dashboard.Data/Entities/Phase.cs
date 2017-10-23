using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Phase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int PhaseId { get; set; }
        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }

        public string PhaseName { get; set; }
        public string Comments { get; set; }
        
        
        public Project Project { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}