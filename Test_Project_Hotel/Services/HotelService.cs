using Test_Project_Hotel.Data;
using Test_Project_Hotel.Models;
using Test_Project_Hotel.ViewModels;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Test_Project_Hotel.Services
{
    public class HotelService
    {
        private HotelContext db;

        public HotelService(HotelContext context)
        {
            db = context;
        }

        public HomeViewModel GetHomeViewModel()
        {
            var clients = db.Clients.ToList();
            var rooms = db.Rooms.ToList();
            var workers = db.Workers.ToList();
            var services = db.Services.Include("Client").Include("Room").Include("Worker").ToList();

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
