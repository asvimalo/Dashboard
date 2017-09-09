using Dashboard.Data.EF.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class User : IdentityUser
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string PersonNr { get; set; }
        public int PictureId { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual IEnumerable<Commitment> Commitment { get; set; }
    }
}