using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.ViewModels;
using Test_Project_Hotel.Infrastructure.Filters;
using Test_Project_Hotel.Infrastructure;
using System;
using Test_Project_Hotel.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Test_Project_Hotel.Controllers
{
    //[TypeFilter(typeof(TimingLogAttribute))] // Фильтр ресурсов
    //[ExceptionFilter] // Фильтр исключений
    public class ServicesController : Controller
    {
        private readonly HotelContext _context;
        private ServiceViewModel _service = new ServiceViewModel
        {
            ServiceName = "",
            ServiceDescription = "",
            ClientFIO = "",
            RoomType = "",
            WorkerFIO = ""
        };

        public ServicesController(HotelContext context)
        {
            _context = context;
        }

        // GET: Services
        [SetToSession("SortState")] //Фильтр действий для сохранение в сессию состояния сортировки
        public IActionResult Index(SortState sortOrder)
        {
            // Считывание данных из сессии
            var sessionService = HttpContext.Session.Get("Service");
            var sessionSortState = HttpContext.Session.Get("SortState");
            if (sessionService != null)
                _service = Transformations.DictionaryToObject<ServiceViewModel>(sessionService);
            if ((sessionSortState != null))
                if ((sessionSortState.Count > 0) & (sortOrder == SortState.No)) sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            // Сортировка и фильтрация данных
            IQueryable<Service> hotelContext = _context.Services;
            hotelContext = Sort_Search(hotelContext, sortOrder, _service.RoomType ?? "", _service.ClientFIO ?? "");

            // Формирование модели для передачи представлению
            _service.SortViewModel = new SortViewModel(sortOrder);
            ServicesViewModel services = new ServicesViewModel
            {
                Services = hotelContext,
                ServiceViewModel = _service
            };
            return View(services);
        }
        // Post: Services
        [HttpPost]
        [SetToSession("Service")] //Фильтр действий для сохранение в сессию параметров отбора
        public IActionResult Index(ServiceViewModel service)
        {
            // Считывание данных из сессии
            var sessionSortState = HttpContext.Session.Get("SortState");
            var sortOrder = new SortState();
            if (sessionSortState.Count > 0)
                sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            // Сортировка и фильтрация данных
            IQueryable<Service> hotelContext = _context.Services;
            hotelContext = Sort_Search(hotelContext, sortOrder, service.RoomType ?? "", service.ClientFIO ?? "");

            // Формирование модели для передачи представлению
            service.SortViewModel = new SortViewModel(sortOrder);
            ServicesViewModel services = new ServicesViewModel
            {
                Services = hotelContext,
                ServiceViewModel = service
            };

            return View(services);
        }
        // Сортировка и фильтрация данных
        private IQueryable<Service> Sort_Search(IQueryable<Service> services, SortState sortOrder, string searchRoomType, string searchClientFio)
        {
            switch (sortOrder)
            {
                case SortState.RoomTypeAsc:
                    services = services.OrderBy(s => s.Room.RoomType);
                    break;
                case SortState.RoomTypeDesc:
                    services = services.OrderByDescending(s => s.Room.RoomType);
                    break;
                case SortState.ClientFioAsc:
                    services = services.OrderBy(s => s.Worker.ClientFIO);
                    break;
                case SortState.ClientFioDesc:
                    services = services.OrderByDescending(s => s.Worker.ClientFIO);
                    break;
            }
            services = services.Include(o => o.Room).Include(o => o.Worker)
                .Where(o => o.Room.RoomType.Contains(searchRoomType ?? "")
                & o.Worker.ClientFIO.Contains(searchClientFio ?? ""));


            return services;
        }


        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(o => o.Worker)
                .Include(o => o.Room)
                .Include(o => o.Worker)
                .SingleOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientFIO");
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType");
            ViewData["WorkerID"] = new SelectList(_context.Workers, "WorkerID", "WorkerFIO");
            return View();
        }

        // POST: Services/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceID,ServiceName,ServiceDescription,EntryDate,DepartureDate,ClientID,WorkerID,RoomID")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientFIO", service.ClientID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", service.RoomID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "WorkerID", "WorkerFIO", service.WorkerID);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services.SingleOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientFIO", service.ClientID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", service.RoomID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "WorkerID", "WorkerFIO", service.WorkerID);
            return View(service);
        }

        // POST: Services/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceID,ServiceName,ServiceDescription,EntryDate,DepartureDate,ClientID,WorkerID,RoomID")] Service service)
        {
            if (id != service.ServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.ServiceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientID"] = new SelectList(_context.Clients, "ClientID", "ClientFIO", service.ClientID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "RoomID", "RoomType", service.RoomID);
            ViewData["WorkerID"] = new SelectList(_context.Workers, "WorkerID", "WorkerFIO", service.WorkerID);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(o => o.Worker)
                .Include(o => o.Room)
                .Include(o => o.Worker)
                .SingleOrDefaultAsync(m => m.ServiceID == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _context.Services.SingleOrDefaultAsync(m => m.ServiceID == id);
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.ServiceID == id);
        }
    }
}
