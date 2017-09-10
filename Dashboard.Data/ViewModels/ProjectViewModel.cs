using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.ViewModel
{
    public class ProjectViewModel
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }

        public ICollection<UserViewModel> Commitments { get; set; }
    }
}
