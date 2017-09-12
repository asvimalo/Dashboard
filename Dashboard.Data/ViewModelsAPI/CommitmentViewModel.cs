using Dashboard.Data.EF.Entities;
using Dashboard.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Data.ViewModelsAPI
{
    public class CommitmentViewModel 
    {
        
        public string Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
