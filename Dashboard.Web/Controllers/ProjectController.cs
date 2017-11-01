using System;
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Angular()
        {
            return View();
        }
        public IActionResult PhonecatApp()
        {
            return View();
        }
    }
}