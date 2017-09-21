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

namespace Dashboard.Web.Controllers
{
    public class CommitmentsController : Controller
    {
        private IHttpClientDashboard _httpClientDashboard;

        public CommitmentsController(IHttpClientDashboard httpClientDashboard)
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
                    var commitmmentsAsString = await responseCom.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<Commitment> commitments = JsonConvert.DeserializeObject<IList<Commitment>>(commitmmentsAsString).ToList();
                    return View(commitments);
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
        public IActionResult AddCommitment()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCommitment([FromBody] Commitment commitment)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedCommitment = JsonConvert.SerializeObject(commitment);
                    var response = await httpClient.PostAsync(
                            $"api/dashboard/commitments",
                            new StringContent(serializedCommitment, System.Text.Encoding.Unicode, "application/json")).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        var commitmmentAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desCommitment = JsonConvert.DeserializeObject<Commitment>(commitmmentAsString);
                        return View(desCommitment);
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
        public async Task<IActionResult> EditCommitment([FromBody] Commitment commitment)
        {
            // TODO
            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedCommitment = JsonConvert.SerializeObject(commitment);

                    var response = await httpClient.PostAsync(
                            $"api/dashboard/commitments",
                            new StringContent(serializedCommitment, 
                                System.Text.Encoding.Unicode, 
                                "application/json"))
                                .ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        var commitmmentAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var desCommitment = JsonConvert.DeserializeObject<Commitment>(commitmmentAsString);
                        return View(desCommitment);
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
        public async Task<IActionResult> DeleteCommitment(int id)
        {
            var httpClient = await _httpClientDashboard.GetClient();
            var response = await httpClient.DeleteAsync($"api/commitments/{id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }
    }
}