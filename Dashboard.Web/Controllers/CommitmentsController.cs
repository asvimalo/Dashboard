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
        public async Task WriteOutIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext.GetTokenAsync("id_token");
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            //.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // write it out
            Debug.WriteLine($"Identity token: {identityToken}");

            // write out the user claims
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }

    }
}