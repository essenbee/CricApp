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
            var result = new List<PlayerViewModel>();

            var testPlayer = new PlayerViewModel
            {
                Pid = 35320,
                FullName = "Sachin Ramesh Tendulkar",
                Country = "India",
                PlayingRole = "Top-order batsman"
            };

            result.Add(testPlayer);

            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }
    }
}
