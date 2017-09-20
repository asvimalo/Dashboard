using Dashboard.Web.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace Dashboard.Web.Services
{
    public class HttpClientDashboard : IHttpClientDashboard
    {
        private IHttpContextAccessor _httpCtxAccessor;
        public HttpClient _httpClient = new HttpClient();

        public HttpClientDashboard(IHttpContextAccessor httpCtxAccessor)
        {
            _httpCtxAccessor = httpCtxAccessor;
        }

        public async Task<HttpClient> GetClient()
        {
            _httpClient.BaseAddress = new Uri("http://localhost:8899");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return _httpClient;
        }
    }
}
