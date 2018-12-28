using System;
using System.Net;
using CricApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CricApp.Controllers
{
    public class PlayerStatsController : Controller
    {
        private IConfiguration _configuration;

        public PlayerStatsController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public IActionResult Index(string pid)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("apikey", _configuration["cricApiKey"]);
            var apiKey = _configuration["cricApiKey"];
            var query = new Uri("https://cricapi.com/api/playerStats?apikey=" + $"{apiKey}&pid={pid}");

            var json = webClient.DownloadString(query);

            var playerStats = JsonConvert.DeserializeObject<PlayerStatsViewModel>(json);


            return View(playerStats);
        }
    }
}