using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Services.Contracts;
using Dashboard.Web.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;

namespace Dashboard.Web.Controllers
{
    public class UsersController : Controller
    {
        private IHttpClientDashboard _httpClientDashboard;

        public UsersController(IHttpClientDashboard httpClientDashboard)
        {
            _httpClientDashboard = httpClientDashboard;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = await _httpClientDashboard.GetClient();
            try
            {
                var responseCom = await httpClient.GetAsync("api/dashboard/commitments").ConfigureAwait(false);
                if (responseCom.IsSuccessStatusCode)
                {
                    var projectsAsString = await responseCom.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<UserModel> projects = JsonConvert.DeserializeObject<IList<UserModel>>(projectsAsString).ToList();
                    return View(projects);
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
                            $"api/dashboard/commitments",
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
    }
}