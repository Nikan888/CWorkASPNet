using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Test_Project_Hotel.ViewModels;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.Infrastructure.Filters;

namespace Test_Project_Hotel.Controllers
{
    [ExceptionFilter]
    [TypeFilter(typeof(TimingLogAttribute))]
    public class HomeController : Controller
    {
        private HotelContext _db;
        public HomeController(HotelContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var clients = _db.Clients.Take(10).ToList();
            var rooms = _db.Rooms.Take(10).ToList();
            var workers = _db.Workers.Take(10).ToList();
            List<ServiceViewModel> services = _db.Services
                .OrderByDescending(d => d.EntryDate)
                .Select(t => new ServiceViewModel { ServiceID = t.ServiceID, ServiceName = t.ServiceName, ServiceDescription = t.ServiceDescription, EntryDate = t.EntryDate, DepartureDate = t.DepartureDate, ClientFIO = t.Client.ClientFIO, RoomType = t.Room.RoomType, WorkerFIO = t.Worker.WorkerFIO })
                .Take(10)
                .ToList();

            HomeViewModel homeViewModel = new HomeViewModel { Clients = clients, Rooms = rooms, Workers = workers, Services = services };
            return View(homeViewModel);
        }
    }
}