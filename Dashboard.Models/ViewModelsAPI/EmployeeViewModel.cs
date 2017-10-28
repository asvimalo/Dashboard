using Dashboard.Entities;
using System.Collections.Generic;

namespace Dashboard.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? PictureId { get; set; }
        public Picture Picture { get; set; }
        public string PersonNr { get; set; }

        public ICollection<CommitmentViewModel> Commitments { get; set; }
    }
}