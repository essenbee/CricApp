using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CricApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CricApp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CricApiController : ControllerBase
    {
        private IConfiguration _configuration;

        public CricApiController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        // GET
        [HttpGet("{id}")]
        public PlayerStatsViewModel GetStats(string id)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("apikey", _configuration["cricApiKey"]);
            var apiKey = _configuration["cricApiKey"];
            var query = new Uri("https://cricapi.com/api/playerStats?apikey=" + $"{apiKey}&pid={id}");

            var json = webClient.DownloadString(query);

            var playerStats = JsonConvert.DeserializeObject<PlayerStatsViewModel>(json);

            if (playerStats is null)
            {
                return null;
            }

            return playerStats;
        }
    }
}