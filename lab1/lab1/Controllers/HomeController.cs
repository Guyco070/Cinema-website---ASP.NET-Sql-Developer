using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace lab1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowHomePage()
        {
            return View("MyHome");
        }

        public ActionResult ShowAction1()
        {
            return View("action1");
        }

        public ActionResult ShowCarPageHome()
        {
            return View("CarPageHome");
        }

        public ActionResult ShowLoginHome()
        {
            ViewBag.Fname = "Guy"+ "\n";
            ViewData["Lname"] = "Cohen";
            ViewBag.PN = "0545679481";
            ViewData["idN"] = "205579808";
            return View("LoginPage");
        }
    }
}