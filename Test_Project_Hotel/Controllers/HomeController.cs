using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Test_Project_Hotel.ViewModels;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.Infrastructure.Filters;

namespace Test_Project_Hotel.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}