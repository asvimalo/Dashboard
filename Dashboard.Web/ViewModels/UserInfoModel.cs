using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Dashboard.Web.ViewModels
{
    public class UserInfoModel
    {
        
        public List<Claim> Claims { get; set; }
    }
}
