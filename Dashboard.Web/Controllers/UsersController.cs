using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Services.Contracts;
using Dashboard.Web.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using Dashboard.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;


namespace Dashboard.Web.Controllers
{
    
    public class EmployeeController : Controller
    {
        private IHttpClientDashboard _httpClientDashboard;

        public EmployeeController(IHttpClientDashboard httpClientDashboard)
        {
            _httpClientDashboard = httpClientDashboard;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            await WriteOutIdentityInformation();
            var httpClient = await _httpClientDashboard.GetClient();
            try
            {
                var responseCom = await httpClient.GetAsync("api/dashboard/employees").ConfigureAwait(false);
                if (responseCom.IsSuccessStatusCode)
                {
                    var usersAsString = await responseCom.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<Employee> users = JsonConvert.DeserializeObject<IList<Employee>>(usersAsString).ToList();
                    return View(users);
                }
                else
                    throw new Exception($"A problem happened while calling the API: {responseCom.ReasonPhrase}");
            }
            catch (Exception ex)
            {

                return Content($"Server down: {ex}");
            }

        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser([FromBody] UserModel user)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedUser = JsonConvert.SerializeObject(user);

                    var response = await httpClient.PostAsync(
                            $"api/dashboard/employees",
                            new StringContent(serializedUser, 
                                System.Text.Encoding.Unicode, 
                                "application/json")).ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var userAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desUser = JsonConvert.DeserializeObject<UserModel>(userAsString);
                        return View(desUser);
                    }
                    else
                        throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
                }
                catch (Exception ex)
                {

                    return Content($"Server down: {ex}");
                }
            }
            else
                return View();

        }
        [Authorize]
        [HttpPut]
        [ValidateAntiForgeryToken] //TO-DO
        public async Task<IActionResult> EditUser([FromBody] UserModel user)
        {
            // TODO
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedUser = JsonConvert.SerializeObject(user);

                    var response = await httpClient.PostAsync(
                            $"api/dashboard/users",
                            new StringContent(serializedUser,
                                System.Text.Encoding.Unicode,
                                "application/json"))
                                .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var userAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desUser = JsonConvert.DeserializeObject<UserModel>(userAsString);
                        return View(desUser);
                    }
                    else
                        throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
                }
                catch (Exception ex)
                {

                    return Content($"Server down: {ex}");
                }
            }
            else
                return View();

        }
        [Authorize]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var httpClient = await _httpClientDashboard.GetClient();
            var response = await httpClient.DeleteAsync($"api/dashboard/users/{id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }
        public async Task<IActionResult> GetUserInfo()
        {
            var discoveryClient = new DiscoveryClient("https://localhost:44394");
            var metaDataResponse = await discoveryClient.GetAsync();

            var userInfoClient = new UserInfoClient(metaDataResponse.UserInfoEndpoint);

            var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var response = await userInfoClient.GetAsync(accessToken);
            if (response.IsError)
            {
                throw new Exception(
                    "Problem accessing the UserInfo endpoint.",
                    response.Exception);
            }
            //var address = response.Claims.FirstOrDefault(x => x.Type == "address")?.Value;
            var claims = response.Claims.ToList();


            var userInfo = new UserInfoModel { Claims = claims };
            //userInfo.Address = address;
            //userInfo.claims = claims;
            return View(userInfo);
        }
        public async System.Threading.Tasks.Task WriteOutIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext.GetTokenAsync("id_token");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            //.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // write it out
            Debug.WriteLine($"Identity token: {identityToken}");
            Debug.WriteLine($"Access token: {accessToken}");

            // write out the user claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"*Claim type: {claim.Type} - Claim value: {claim.Value}");

            }


        }
    }
}