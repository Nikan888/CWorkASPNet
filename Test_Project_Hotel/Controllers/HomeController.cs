using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.Controllers
{
    public class HomeController : Controller
    {
        private HotelContext db;
        public HomeController(HotelContext _db)
        {
            db = _db;
        }
        public IActionResult Index()
        {
            var client = db.Clients.ToList();

            return View(client);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
