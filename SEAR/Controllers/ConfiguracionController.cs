using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SEAR.DTO;
using SEAR.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;

namespace SEAR.Controllers
{
    public class ConfiguracionController : Controller
    {
        IConfiguration _configuration;
        public ConfiguracionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> saveConfigurationForm(string ip, string modo)
        {
            var client = new HttpClient();
            string url = $"http://{ip}/{modo}";

            ViewBag.url = url;
            ViewBag.ip = ip;

            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
