using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.ViewModel
{
    public class ActivityViewModel
    {
        public int ActivityId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StopDate { get; set; }

        public ICollection<UserViewModel> users { get; set; }
    }
}
