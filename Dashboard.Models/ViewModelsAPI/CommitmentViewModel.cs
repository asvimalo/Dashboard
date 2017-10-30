﻿using Dashboard.Entities;


namespace Dashboard.Models
{
    public class CommitmentViewModel 
    {
        
        public string Name { get; set; }
        public int UserId { get; set; }
        public Employee User { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}