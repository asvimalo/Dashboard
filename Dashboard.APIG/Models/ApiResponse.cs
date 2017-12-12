using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dashboard.APIG.Models
{
    public class ApiResponse<TEntity> 
    {
        public bool Status { get; set; }
        public TEntity Entity { get; set; }
        public ModelStateDictionary ModelState { get; set; }
    }
}
