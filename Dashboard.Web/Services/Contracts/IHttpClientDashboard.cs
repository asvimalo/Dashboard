using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Dashboard.Web.Services.Contracts
{
    public interface IHttpClientDashboard
    {
        Task<HttpClient> GetClient();
    }
}
