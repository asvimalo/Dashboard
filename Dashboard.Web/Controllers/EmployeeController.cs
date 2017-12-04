using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Services.Contracts;
using Dashboard.Web.ViewModels;
using Newtonsoft.Json;
using System.Net.Http;

using Microsoft.AspNetCore.Authorization;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using Dashboard.EntitiesG.EntitiesRev;

namespace Dashboard.Web.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}