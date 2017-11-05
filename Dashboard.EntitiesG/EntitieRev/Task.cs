using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.EntitiesG.EntitiesRev
{
    [Table("Task")]
    public class Task 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public int PhaseId { get; set; }       
        [ForeignKey("PhaseId")]
        public Phase Phase { get; set; }
    }
}