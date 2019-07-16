using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Capstone.Web.Controllers
{
    public class SurveyController : Controller
    {
        private SurveyResultDAL dao;

        public SurveyController(SurveyResultDAL dao)
        {
            this.dao = dao;
        }

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSurvey(SurveyResult survey)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", survey);
            }

            dao.InsertSurvey(survey);

            return RedirectToAction("FavoriteParks");
        }

        public IActionResult FavoriteParks()
        {
            List<Park> parks = new List<Park>();
            parks = dao.TopParks();
            return View(parks);
        }
    }
}