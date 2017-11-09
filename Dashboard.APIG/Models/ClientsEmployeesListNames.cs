using Dashboard.EntitiesG.EntitiesRev;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.APIG.Models
{
    public class ClientsEmployeesListNames
    {
        public IQueryable<Client> Clients { get; set; }
        public IQueryable<Employee> Employees { get; set; }
    }
}
