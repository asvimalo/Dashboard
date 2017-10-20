using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Services.Contracts;
using Newtonsoft.Json;
using System.Net.Http;
using Dashboard.Web.ViewModels;
using Dashboard.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;


namespace Dashboard.Web.Controllers
{
    [Authorize]
    public class AssignmentsController : Controller
    {
        private IHttpClientDashboard _httpClientDashboard;

        public AssignmentsController(IHttpClientDashboard httpClientDashboard)
        {
            _httpClientDashboard = httpClientDashboard;
        }
        public async Task<IActionResult> Index()
        {
            
            var httpClient = await _httpClientDashboard.GetClient();
            try
            {
                var responseCom = await httpClient.GetAsync("api/dashboard/assignments").ConfigureAwait(false);
                if (responseCom.IsSuccessStatusCode)
                {
                    var commitmmentsAsString = await responseCom.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<Assignment> assignments = JsonConvert.DeserializeObject<IList<Assignment>>(commitmmentsAsString).ToList();
                    return View(assignments);
                }
                else
                    throw new Exception($"A problem happened while calling the API: {responseCom.ReasonPhrase}");
            }
            catch (Exception ex)
            {

                return Content($"Server down: {ex}");
            }

        }

        [HttpGet]
        public IActionResult AddAssignment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAssignment([FromBody] Assignment assignment)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedAssignment = JsonConvert.SerializeObject(assignment);
                    var response = await httpClient.PostAsync(
                            $"api/dashboard/assignments",
                            new StringContent(serializedAssignment, System.Text.Encoding.Unicode, "application/json")).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var AssignmentAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desAssignment = JsonConvert.DeserializeObject<Assignment>(AssignmentAsString);
                        return View(desAssignment);
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
        public async Task<IActionResult> EditAssigment([FromBody] Assignment assignment)
        {
            // TODO
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedAssignment = JsonConvert.SerializeObject(assignment);

                    var response = await httpClient.PostAsync(
                            $"api/dashboard/assignments",
                            new StringContent(serializedAssignment, 
                                System.Text.Encoding.Unicode, 
                                "application/json"))
                                .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var assignmentAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desAssignment = JsonConvert.DeserializeObject<Assignment>(assignmentAsString);
                        return View(desAssignment);
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
        public async Task<IActionResult> DeleteAssignment(int id)
        {
            var httpClient = await _httpClientDashboard.GetClient();
            var response = await httpClient.DeleteAsync($"api/dashboard/assignment/{id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }
        
        

    }
}