using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Worker> Workers { get; set; }
        public IEnumerable<ServiceViewModel> Services { get; set; }



    }
}
