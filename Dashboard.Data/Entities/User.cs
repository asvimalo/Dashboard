using Dashboard.Data.EF.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class User : BaseEntity
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string PersonNr { get; set; }
        public virtual IEnumerable<Project> Projects { get; set; }
    }
}