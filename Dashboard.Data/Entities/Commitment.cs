using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Dashboard.Data.EF.Entities;

namespace Dashboard.Data.Entities
{
    public class Commitment : BaseEntity
    {
        
        public string Name { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }       
        public Project Project { get; set; }

    }
}
