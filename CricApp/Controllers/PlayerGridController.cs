using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using CricApp.Models;
using Kendo.Mvc.Extensions;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace CricApp.Controllers
{
    public class PlayerGridController : Controller
    {
        private IConfiguration _configuration;

        public PlayerGridController(IConfiguration Configuration, CricApiController api)
        {
            _configuration = Configuration;
        }

        public ActionResult Players_Read([DataSourceRequest]DataSourceRequest request)
        {
            var sql = @"select CricApiId as Pid, Name, Country, PlayingRole from Players";
            var result = new List<PlayerViewModel>();

            using (var connection = new SqlConnection(_configuration["dbConnStr"]))
            {
                result = connection.Query<PlayerViewModel>(sql).ToList();
            }

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }
    }
}
