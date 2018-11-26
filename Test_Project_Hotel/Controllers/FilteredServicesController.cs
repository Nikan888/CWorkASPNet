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

namespace Test_Project_Hotel.Controllers
{
    //[ExceptionFilter] // Фильтр исключений
    public class FilteredServicesController : Controller
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

        public FilteredServicesController(HotelContext context)
        {
            _context = context;
        }

        // GET: Services
        [SetToSession("SortState")] //Фильтр действий для сохранение в сессию состояния сортировки
        public async Task<IActionResult> Index(SortState sortOrder, int page = 1)
        {
            int pageSize = 3;
            // Считывание данных из сессии
            var sessionService = HttpContext.Session.Get("Service");
            var sessionSortState = HttpContext.Session.Get("SortState");
            if (sessionService != null)
                _service = Transformations.DictionaryToObject<ServiceViewModel>(sessionService);
            if ((sessionSortState != null))
                if ((sessionSortState.Count > 0) & (sortOrder == SortState.No)) sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionSortState["sortOrder"]);

            // Сортировка и фильтрация данных
            IQueryable<Service> hotelContext = _context.Services;
            hotelContext = Sort_Search(hotelContext, sortOrder, _service.ClientFIO ?? "", _service.RoomType ?? "");

            var count = await hotelContext.CountAsync();
            var items = await hotelContext.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            //PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);

            // Формирование модели для передачи представлению
            _service.SortViewModel = new SortViewModel(sortOrder);
            ServicesViewModel services = new ServicesViewModel
            {
                //PageViewModel = pageViewModel;
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
            hotelContext = Sort_Search(hotelContext, sortOrder, service.ClientFIO ?? "", service.RoomType ?? "");

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
        private IQueryable<Service> Sort_Search(IQueryable<Service> services, SortState sortOrder, string searchClientFio, string searchRoomType)
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
                    services = services.OrderBy(s => s.Client.ClientFIO);
                    break;
                case SortState.ClientFioDesc:
                    services = services.OrderByDescending(s => s.Client.ClientFIO);
                    break;
            }
            services = services.Include(o => o.Worker).Include(o => o.Room).Include(o => o.Worker)
                .Where(o => o.Client.ClientFIO.Contains(searchClientFio ?? "")
                & o.Room.RoomType.Contains(searchRoomType ?? ""));



            return services;
        }



    }
}
