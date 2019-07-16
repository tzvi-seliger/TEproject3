using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Capstone.Web.Models;
using Capstone.Web.DAL;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Capstone.Web.Controllers
{
    public class HomeController : Controller
    {
        private ParkDAL dao;

        public HomeController (ParkDAL dao)
        {
            this.dao = dao;
        }

        public IActionResult Index()
        {
            List<Park> parks = new List<Park>();
            parks = dao.GetParks();
            return View(parks);
        }

        public IActionResult Detail(string parkCode)
        {
            Park park = dao.GetPark(parkCode, GetActiveTempUnit());
            return View(park);
        }

        [HttpPost]
        public IActionResult AssignTempUnit (string tempUnit = "f", string parkCode = "") //also pass in parkCode
        {
            Park park = dao.GetPark(parkCode, GetActiveTempUnit());
            SaveTempUnit(tempUnit);
            return RedirectToAction("Detail", park);

        }

        public string GetActiveTempUnit()   
        {

            if (HttpContext.Session.GetString("Temp_Unit") == null)
            {
                SaveTempUnit("f");
                return "f";
            }
            else
            {
                return HttpContext.Session.GetString("Temp_Unit");
            }

        }


        public void SaveTempUnit (string tempUnit)
        {
            HttpContext.Session.SetString("Temp_Unit", tempUnit.ToLower());
        }

               
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
