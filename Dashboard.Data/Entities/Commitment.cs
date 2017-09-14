using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Commitment 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommitmentId { get; set; }
        [Required]
        public string Name { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public int ProjectId { get; set; }
        [ForeignKey("UserId")]
        public Project Project { get; set; }

    }
}
