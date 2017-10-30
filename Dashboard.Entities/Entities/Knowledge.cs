﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Dashboard.Entities
{
    
    public class Knowledge
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KnowledgeId { get; set; }
        public string Description { get; set; }
        public string KnowledgeName { get; set; }
        public ICollection <AcquiredKnowledge> AcquiredKnowledges { get; set; }
    }
}