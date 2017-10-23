﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dashboard.Data.Entities
{
    
    public class Knowledge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int KnowledgeId { get; set; }

        public string Description { get; set; }
        public string KnowledgeName { get; set; }

        //public ICollection <KnowledgeEmployee> KnowledgeEmployees { get; set; } ??AquiredKnowledge
    }
}
