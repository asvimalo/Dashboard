using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.EntitiesG.EntitiesRev
{
    [Table("Location")]
    public class Location : BaseEntity
    {
        
        public string City { get; set; }
        public string Address { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}