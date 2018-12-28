using CricApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CricApp.Controllers
{
    public class PlayerStatsController : Controller
    {
        private IConfiguration _configuration;
        private CricApiController _api;

        public PlayerStatsController(IConfiguration Configuration, CricApiController api)
        {
            _configuration = Configuration;
            _api = api;
        }

        public IActionResult Index(string pid)
        {
            var err = new PlayerStatsViewModel { Name = "Not Found", FullName = "-" };
            var result = _api.GetStats(pid);

            if (result is OkObjectResult)
            {
                var ok = result as OkObjectResult;
                var playerStats = JsonConvert.DeserializeObject<PlayerStatsViewModel>(ok.Value.ToString());
                return View(playerStats);
            }

            return View(err);
        }
    }
}