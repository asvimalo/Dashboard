using Dashboard.Data.EF.Entities;

namespace Dashboard.Data.Entities
{
    public class Commitment : BaseEntity
    {
        public int CommitmentId { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }       
        public Project Project { get; set; }

    }
}
