using System;
using System.Collections.Generic;


namespace Dashboard.Data.ViewModelsAPI
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
