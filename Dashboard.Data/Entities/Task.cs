using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Task
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int TaskId { get; set; }
        [ForeignKey("PhaseId")]
        public int PhaseId { get; set; }
        
        public string TaskName { get; set; }

        public Phase Phase { get; set; }
    }
}