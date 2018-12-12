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

        public async Task<IActionResult> Index(int? worker, string name, int page = 1, ServiceSortState sortOrder = ServiceSortState.NameAsc)
        {
            int pageSize = 5;

            //фильтрация
            IQueryable<Service> services = db.Services.Include(x => x.Client).Include(x => x.Room).Include(x => x.Worker);

            if (worker != null && worker != 0)
            {
                services = services.Where(p => p.WorkerID == worker);
            }
            if (!String.IsNullOrEmpty(name))
            {
                services = services.Where(p => p.ServiceName.Contains(name));
            }

            // сортировка
            switch (sortOrder)
            {
                case ServiceSortState.NameDesc:
                    services = services.OrderByDescending(s => s.ServiceName);
                    break;
                case ServiceSortState.ClientFIOAsc:
                    services = services.OrderBy(s => s.Client.ClientFIO);
                    break;
                case ServiceSortState.ClientFIODesc:
                    services = services.OrderByDescending(s => s.Client.ClientFIO);
                    break;
                case ServiceSortState.RoomTypeAsc:
                    services = services.OrderBy(s => s.Room.RoomType);
                    break;
                case ServiceSortState.RoomTypeDesc:
                    services = services.OrderByDescending(s => s.Room.RoomType);
                    break;
                case ServiceSortState.WorkerFIOAsc:
                    services = services.OrderBy(s => s.Worker.WorkerFIO);
                    break;
                case ServiceSortState.WorkerFIODesc:
                    services = services.OrderByDescending(s => s.Worker.WorkerFIO);
                    break;
                default:
                    services = services.OrderBy(s => s.ServiceName);
                    break;
            }

            // пагинация
            var count = await services.CountAsync();
            var items = await services.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            ServicesViewModel viewModel = new ServicesViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                ServiceSortViewModel = new ServiceSortViewModel(sortOrder),
                ServiceFilterViewModel = new ServiceFilterViewModel(name, db.Workers.ToList(), worker), 
                Services = items
            };
            return View(viewModel);
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
                Dictionary<string, int> rooms = new Dictionary<string, int>();
                Dictionary<string, int> workers = new Dictionary<string, int>();
                foreach (var item in db.Clients)
                {
                    clients.Add(item.ClientFIO, item.ClientID);
                }
                foreach (var item in db.Rooms)
                {
                    rooms.Add(item.RoomType, item.RoomID);
                }
                foreach (var item in db.Workers)
                {
                    workers.Add(item.WorkerFIO, item.WorkerID);
                }
                EditServiceViewModel model = new EditServiceViewModel
                {
                    Id = service.ServiceID,
                    ServiceName = service.ServiceName,
                    ServiceDescription = service.ServiceDescription,
                    EntryDate = service.EntryDate,
                    DepartureDate = service.DepartureDate,
                    ClientID = service.Client.ClientID,
                    RoomID = service.Room.RoomID,
                    WorkerID = service.Worker.WorkerID
                };
                ViewBag.clients = clients;
                ViewBag.rooms = rooms;
                ViewBag.workers = workers;
                return View(model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Error! Missing id in query parameters" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditServiceViewModel model)
        {
            try
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
                service.ServiceName = model.ServiceName;
                service.ServiceDescription = model.ServiceDescription;
                service.EntryDate = model.EntryDate;
                service.DepartureDate = model.DepartureDate;
                service.ClientID = model.ClientID;
                service.RoomID = model.RoomID;
                service.WorkerID = model.ClientID;
                ViewBag.clients = clients;
                ViewBag.workers = workers;
                ViewBag.rooms = rooms;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

            }
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
