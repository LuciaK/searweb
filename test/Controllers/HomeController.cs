using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using test.DTO;
using test.Models;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace test.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _configuration;
        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            var client = new WebClient();
            var result = client.DownloadString(_configuration["HostUrl"]);
            var obj = JsonConvert.DeserializeObject<JsonAlimentador>(result);

            ViewBag.porcionBase = 25; 
            ViewBag.petSrc = _configuration["PetSrc"];

            return View(obj);
        }
        public async Task<IActionResult> saveConfiguration(string nombreMascota, string hora1, string hora2, string hora3, string hora4, int porcion)
        {
            var client = new HttpClient();
            var data = new JsonAlimentador
            {
                nombreMascota = nombreMascota,
                hora1 = hora1,
                hora2 = hora2,
                hora3 = hora3,
                hora4 = hora4,
                porcion = porcion
            };

            var response = await client.PutAsJsonAsync(_configuration["HostUrl"], data);
            response.EnsureSuccessStatusCode();


            return RedirectToAction("Index");
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
