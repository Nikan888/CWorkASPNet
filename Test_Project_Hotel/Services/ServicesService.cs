using Test_Project_Hotel.Data;
using Test_Project_Hotel.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Test_Project_Hotel.Services
{
    // Класс выборки 10 записей из всех таблиц 
    public class ServicesService
    {
        private HotelContext _context;
        public ServicesService(HotelContext context)
        {
            _context = context;
        }
        public HomeViewModel GetHomeViewModel()
        {
            var clients = _context.Clients.Take(10).ToList();
            var rooms = _context.Rooms.Take(10).ToList();
            var workers = _context.Workers.Take(10).ToList();
            List<ServiceViewModel> services = _context.Services
                .OrderByDescending(d => d.EntryDate)
                .Select(t => new ServiceViewModel
                {
                    ServiceID = t.ServiceID,
                    ServiceName = t.ServiceName,
                    ServiceDescription = t.ServiceDescription,
                    EntryDate = t.EntryDate,
                    DepartureDate = t.DepartureDate,
                    ClientFIO = t.Worker.ClientFIO,
                    RoomType = t.Room.RoomType,
                    WorkerFIO = t.Worker.WorkerFIO
                })
                .Take(10)
                .ToList();

            HomeViewModel homeViewModel = new HomeViewModel
            {
                Clients = clients,
                Rooms = rooms,
                Workers = workers,
                Services = services
            };
            return homeViewModel;
        }
    }
}
