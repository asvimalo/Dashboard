using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Services.Contracts;
using Dashboard.Web.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;
using Dashboard.Entities;

namespace Dashboard.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private IHttpClientDashboard _httpClientDashboard;

        public ProjectsController(IHttpClientDashboard httpClientDashboard)
        {
            _httpClientDashboard = httpClientDashboard;
        }
        public async Task<IActionResult> Index()
        {
            var httpClient = await _httpClientDashboard.GetClient();
            try
            {
                var responseProj = await httpClient.GetAsync("api/dashboard/projects").ConfigureAwait(false);
                if (responseProj.IsSuccessStatusCode)
                {
                    var projectsAsString = await responseProj.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<Project> projects = JsonConvert.DeserializeObject<IList<Project>>(projectsAsString).ToList();
                    return View(projects);
                }
                else
                    throw new Exception($"A problem happened while calling the API: {responseProj.ReasonPhrase}");
            }
            catch (Exception ex)
            {

                return Content($"Server down: {ex}");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProject([FromBody] ProjectModel project)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedProject = JsonConvert.SerializeObject(project);
                    var response = await httpClient.PostAsync(
                            $"api/dashboard/projects",
                            new StringContent(serializedProject, 
                            System.Text.Encoding.Unicode, 
                            "application/json"))
                            .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var projectAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desproject = JsonConvert.DeserializeObject<Project>(projectAsString);
                        return View(desproject);
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
        public async Task<IActionResult> EditProject([FromBody] Project project)
        {
            // TODO
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedProject = JsonConvert.SerializeObject(project);

                    var response = await httpClient.PostAsync(
                            $"api/dashboard/projects",
                            new StringContent(serializedProject,
                                System.Text.Encoding.Unicode,
                                "application/json"))
                                .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var projectAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desProject = JsonConvert.DeserializeObject<Project>(projectAsString);
                        return View(desProject);
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
        public async Task<IActionResult> DeleteProject(int id)
        {
            var httpClient = await _httpClientDashboard.GetClient();
            var response = await httpClient.DeleteAsync($"api/projects/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }
    }
}