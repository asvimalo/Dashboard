using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.EntitiesG.EntitiesRev
{
    [Table("Client")]
    public class Client : BaseEntity
    {
        public int LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public string ClientName { get; set; }
        public string Description { get; set; }
       
        public ICollection<Project> Projects { get; set; }

    }
}