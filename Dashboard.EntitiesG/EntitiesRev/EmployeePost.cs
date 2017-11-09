
using System;
using System.Collections.Generic;
using System.Text;

namespace Dashboard.EntitiesG.EntitiesRev
{
    public class EmployeePost
    {
        //public byte[] File { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonNr { get; set; }
        public List<string> newKnowledges { get; set; }
        public ICollection<Knowledge> knowledges { get; set; }

    }

}
