using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class Location
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}