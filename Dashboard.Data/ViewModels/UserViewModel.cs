using Dashboard.Data.Entities;
using System.Collections.Generic;

namespace Dashboard.Data.ViewModel
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int PictureId { get; set; }
        public Picture Picture { get; set; }
        public string PersonNr { get; set; }

        public ICollection<Project> Commitments { get; set; }
    }
}