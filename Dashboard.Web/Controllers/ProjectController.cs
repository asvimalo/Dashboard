using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Dashboard.Web.Services.Contracts;

using Newtonsoft.Json;
using System.Net.Http;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Dashboard.Web.ViewModels;
using System.Diagnostics;
using Dashboard.EntitiesG.EntitiesRev;

public class ProjectController : Controller
{
    public IActionResult AngApp()
    {
        return View();
    }

    public IActionResult Index()
    {
        return View();
    }
}

   