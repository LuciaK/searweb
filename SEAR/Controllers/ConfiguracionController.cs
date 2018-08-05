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
            var wclient = new WebClient();
            var result = wclient.DownloadString(_configuration["HostUrl"]);
            var obj = JsonConvert.DeserializeObject<JsonAlimentador>(result);
            ViewBag.modo = obj.modo;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> saveConfigurationForm(string modo)
        {
            var client = new HttpClient();

            var wclient = new WebClient();
            var result = wclient.DownloadString(_configuration["HostUrl"]);
            var obj = JsonConvert.DeserializeObject<JsonAlimentador>(result);


            var data = new JsonAlimentador
            {
                alerta = obj.alerta,
                hora1 = obj.hora1,
                hora2 = obj.hora2,
                hora3 = obj.hora3,
                hora4 = obj.hora4,
                modo = modo,
                nombreMascota = obj.nombreMascota,
                porcion = obj.porcion
            };

            var response = await client.PutAsJsonAsync(_configuration["HostUrl"], data);
            response.EnsureSuccessStatusCode();
            ViewBag.modo = modo;

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
