using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        //[Key]
        public int ClientId { get; set; }
        [ForeignKey("LocationId")]
        public int LocationId { get; set; }

        public string ClientName { get; set; }
        public string Description { get; set; }
        

        public Location Location { get; set; }
        //public ICollection<Project> Projects { get; set; }

    }
}