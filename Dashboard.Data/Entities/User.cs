using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace Dashboard.Data.Entities
{
    public class User 
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }    
        public string PersonNr { get; set; }

        public int? PictureId { get; set; }
        [ForeignKey("PictureId")]
        public  Picture Picture { get; set; }
      
        public ICollection <Commitment> Commitments { get; set; }
    }
}