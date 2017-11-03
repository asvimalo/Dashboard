using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.Entities.Entities
{
    public class EmployeePost
    {
        public byte[] Bytes { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNr { get; set; }
        public ICollection<AcquiredKnowledge> AcquiredKnowledge { get; set; }
        public ICollection<Assignment> Assignments { get; set; }

    }

}
