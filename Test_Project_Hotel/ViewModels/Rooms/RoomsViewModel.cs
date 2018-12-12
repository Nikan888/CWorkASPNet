using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.ViewModels.Rooms
{
    public class RoomsViewModel
    {
        //public IEnumerable<RoomViewModel> Rooms { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public RoomFilterViewModel RoomFilterViewModel { get; set; }
        public RoomSortViewModel RoomSortViewModel { get; set; }
    }
}
