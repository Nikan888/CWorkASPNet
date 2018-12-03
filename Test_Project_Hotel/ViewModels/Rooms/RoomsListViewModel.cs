using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Rooms
{
    public class RoomsListViewModel
    {
        public IEnumerable<RoomViewModel> Rooms { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
