﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Services.Contracts;
using Dashboard.Entities;
using Newtonsoft.Json;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Dashboard.Web.ViewModels;
using System.Diagnostics;

namespace Dashboard.Web.Controllers
{
    public class ProjectController : Controller
    {
        private IHttpClientDashboard _httpClientDashboard;

        public ProjectController(IHttpClientDashboard httpClientDashboard)
        {
            _httpClientDashboard = httpClientDashboard;
        }
        public async Task<IActionResult> Index()
        {
            //await WriteOutIdentityInformation();
            var httpClient = await _httpClientDashboard.GetClient();
            try
            {
                var responseProject = await httpClient.GetAsync("api/dashboard/projects").ConfigureAwait(false);
                if (responseProject.IsSuccessStatusCode)
                {
                    var projectsAsString = await responseProject.Content.ReadAsStringAsync().ConfigureAwait(false);
                    List<Project> employees = JsonConvert.DeserializeObject<IList<Project>>(projectsAsString).ToList();
                    return View(employees);
                }
                else
                    throw new Exception($"A problem happened while calling the API: {responseProject.ReasonPhrase}");
            }
            catch (Exception ex)
            {

                return Content($"Server down: {ex}");
            }

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([FromBody] Project project)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedproject = JsonConvert.SerializeObject(project);

                    var response = await httpClient.PostAsync(
                            $"api/dashboard/projects",
                            new StringContent(serializedproject,
                                System.Text.Encoding.Unicode,
                                "application/json")).ConfigureAwait(false);

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
                return View("Index");

        }
        [HttpGet]
        public IActionResult Edit()
        {
            return View();
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Project project)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var httpClient = await _httpClientDashboard.GetClient();
                    var serializedEmployee = JsonConvert.SerializeObject(project);

                    var response = await httpClient.PutAsync(
                            $"api/dashboard/employees",
                            new StringContent(serializedEmployee,
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

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = await _httpClientDashboard.GetClient();
            var response = await httpClient.DeleteAsync($"api/dashboard/projects/{id}").ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            throw new Exception($"A problem happened while calling the API: {response.ReasonPhrase}");
        }
        //OPENID CONNECT
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
        //WRITE INFO
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

        public IActionResult Angular()
        {
            return View();
        }
    }
}