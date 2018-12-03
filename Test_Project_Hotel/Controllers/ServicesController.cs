using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Project_Hotel.Models;
using Test_Project_Hotel.ViewModels;
using Test_Project_Hotel.ViewModels.Services;

namespace Test_Project_Hotel.Controllers
{
    [Authorize]
    public class ServicesController : Controller
    {
        HotelContext db;

        public ServicesController(HotelContext context)
        {
            db = context;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            List<ServiceViewModel> list = new List<ServiceViewModel>();
            var services = db.Services.Include("Client").Include("Worker").Include("Room").ToList();
            foreach (var service in services)
            {
                list.Add(new ServiceViewModel
                {
                    Id = service.ServiceID,
                    ServiceName = service.ServiceName,
                    ServiceDescription = service.ServiceDescription,
                    EntryDate = service.EntryDate,
                    DepartureDate = service.DepartureDate,
                    ClientFIO = service.Client.ClientFIO,
                    RoomType = service.Room.RoomType,
                    WorkerFIO = service.Worker.WorkerFIO
                });
            }
            IQueryable<ServiceViewModel> filterList = list.AsQueryable();
            var count = filterList.Count();
            var items = filterList.Skip((page - 1) * pageSize).
                Take(pageSize).ToList();
            ServicesListViewModel model = new ServicesListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Services = items
            };
            return View(model);
        }

        public IActionResult Create()
        {
            Dictionary<string, int> clients = new Dictionary<string, int>();
            Dictionary<string, int> workers = new Dictionary<string, int>();
            Dictionary<string, int> rooms = new Dictionary<string, int>();
            foreach (var item in db.Clients)
            {
                clients.Add(item.ClientFIO, item.ClientID);
            }
            foreach (var item in db.Workers)
            {
                workers.Add(item.WorkerFIO, item.WorkerID);
            }
            foreach (var item in db.Rooms)
            {
                rooms.Add(item.RoomType, item.RoomID);
            }
            ViewBag.clients = clients;
            ViewBag.workers = workers;
            ViewBag.rooms = rooms;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Service model)
        {
            if (ModelState.IsValid)
            {
                Service service = new Service()
                {
                    ServiceName = model.ServiceName,
                    ServiceDescription = model.ServiceDescription,
                    EntryDate = model.EntryDate,
                    DepartureDate = model.DepartureDate,
                    ClientID = model.ClientID,
                    WorkerID = model.WorkerID,
                    RoomID = model.RoomID
                };
                await db.Services.AddAsync(service);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Service service = await db.Services.FirstOrDefaultAsync(t => t.ServiceID == id);
                if (service == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! There is no record in the database with the id passed = " + id
                    };
                    return View("Error", error);
                }
                Dictionary<string, int> clients = new Dictionary<string, int>();
                Dictionary<string, int> workers = new Dictionary<string, int>();
                Dictionary<string, int> rooms = new Dictionary<string, int>();
                foreach (var item in db.Clients)
                {
                    clients.Add(item.ClientFIO, item.ClientID);
                }
                foreach (var item in db.Workers)
                {
                    workers.Add(item.WorkerFIO, item.WorkerID);
                }
                foreach (var item in db.Rooms)
                {
                    rooms.Add(item.RoomType, item.RoomID);
                }
                EditServiceViewModel model = new EditServiceViewModel
                {
                    Id = service.ServiceID,
                    ServiceName = service.ServiceName,
                    ServiceDescription = service.ServiceDescription,
                    EntryDate = service.EntryDate,
                    DepartureDate = service.DepartureDate,
                    ClientID = service.ClientID,
                    WorkerID = service.WorkerID,
                    RoomID = service.RoomID
                };
                ViewBag.clients = clients;
                ViewBag.workers = workers;
                ViewBag.rooms = rooms;
                return View(model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Error! Missing id in request parameters" };
                return View("Error", error);
            }
        }

        public async Task<IActionResult> Edit(EditServiceViewModel model)
        {
            Service service = await db.Services.Include("Client").Include("Worker").Include("Room").FirstOrDefaultAsync(t => t.ServiceID == model.Id);
            if (ModelState.IsValid)
            {
                if (service == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! Empty model sent"
                    };
                    return View("Error", error);
                }
            }
            Dictionary<string, int> clients = new Dictionary<string, int>();
            Dictionary<string, int> workers = new Dictionary<string, int>();
            Dictionary<string, int> rooms = new Dictionary<string, int>();
            foreach (var item in db.Clients)
            {
                clients.Add(item.ClientFIO, item.ClientID);
            }
            foreach (var item in db.Workers)
            {
                workers.Add(item.WorkerFIO, item.WorkerID);
            }
            foreach (var item in db.Rooms)
            {
                rooms.Add(item.RoomType, item.RoomID);
            }
            ViewBag.clients = clients;
            ViewBag.workers = workers;
            ViewBag.rooms = rooms;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Service service = await db.Services.FirstOrDefaultAsync(t => t.ServiceID == id);
            db.Services.Remove(service);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ErrorViewModel error = new ErrorViewModel
            {
                RequestId = "Admin access only"
            };
            return View("Error", error);
        }
    }
}
