using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Kendo.Mvc.UI;
using CricApp.Models;
using Kendo.Mvc.Extensions;

namespace CricApp.Controllers
{
    public class PlayerGridController : Controller
    {
        public ActionResult Players_Read([DataSourceRequest]DataSourceRequest request)
        {
            var result = new List<PlayerViewModel>(); //308967

            var testPlayer = new PlayerViewModel
            {
                Pid = 35320,
                Name = "Sachin Tendulkar",
                Country = "India",
                PlayingRole = "Top-order batsman"
            };

            var testPlayer2 = new PlayerViewModel
            {
                Pid = 308967,
                Name = "Jos Buttler",
                Country = "England",
                PlayingRole = "Wicketkeeper batsman"
            };

            var testPlayer3 = new PlayerViewModel
            {
                Pid = 303669,
                Name = "Joe Root",
                Country = "England",
                PlayingRole = "Top-order batsman"
            };

            var testPlayer4 = new PlayerViewModel
            {
                Pid = 8608,
                Name = "Jimmy Anderson",
                Country = "England",
                PlayingRole = "Bowler"
            };

            result.Add(testPlayer);
            result.Add(testPlayer2);
            result.Add(testPlayer3);
            result.Add(testPlayer4);

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }
    }
}
