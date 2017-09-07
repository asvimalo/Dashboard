using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Dashboard.Data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PictureId { get; set; }
        [ForeignKey("PictureId")]
        public Picture Picture { get; set; }
        public string PersonNr { get; set; }

        public ICollection<Commitment> Commitments { get; set; }
    }
}