using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using CricApp.Models;
using Kendo.Mvc.Extensions;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using Serilog;
using Dapper.Contrib.Extensions;

namespace CricApp.Controllers
{
    public class PlayerGridController : Controller
    {
        private IConfiguration _configuration;

        public PlayerGridController(IConfiguration Configuration, CricApiController api)
        {
            _configuration = Configuration;
        }

        public ActionResult GetPlayers([DataSourceRequest]DataSourceRequest request)
        {
            var sql = @"select Id, CricApiId, Name, Country, PlayingRole from Players";
            var result = new List<PlayerViewModel>();

            try
            {
                using (var connection = new SqlConnection(_configuration["dbConnStr"]))
                {
                    result = connection.QueryAsync<PlayerViewModel>(sql)
                        .Result
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                Log.Error($"GetPlayers(): {ex.Message}");
            }

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpPost]
        public ActionResult EditPlayer([DataSourceRequest] DataSourceRequest request, PlayerViewModel player)
        {
            if (ModelState.IsValid && player != null)
            {
                try
                {
                    using (var connection = new SqlConnection(_configuration["dbConnStr"]))
                    {
                        connection.Open();

                        var playerToUpdate = connection.Get<PlayerViewModel>(player.Id);

                        playerToUpdate.CricApiId = player.CricApiId;
                        playerToUpdate.Name = player.Name;
                        playerToUpdate.Country = player.Country;
                        playerToUpdate.PlayingRole = player.PlayingRole;

                        var success = connection.Update(playerToUpdate);

                        if (success)
                        {
                            var result = new[] { playerToUpdate }.ToDataSourceResult(request, ModelState);
                            return Json(result);
                        }

                        Log.Error($"EditPlayer(): update of record {player.Id} failed!");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"EditPlayer(): {ex.Message}");
                }
            }

            var res = new[] { player }.ToDataSourceResult(request, ModelState);

            return Json(res);
        }
    }
}
