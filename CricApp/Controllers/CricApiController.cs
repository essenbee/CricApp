using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(404)]
        public IActionResult GetStats(string id)
        {
            var webClient = new WebClient();
            webClient.Headers.Add("apikey", _configuration["cricApiKey"]);
            var apiKey = _configuration["cricApiKey"];
            var query = new Uri("https://cricapi.com/api/playerStats?apikey=" + $"{apiKey}&pid={id}");

            var json = webClient.DownloadString(query);

            if (json is null)
            {
                return NotFound();
            }

            return Ok(json);
        }
    }
}