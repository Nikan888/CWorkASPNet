using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Rooms
{
    public class RoomFilterViewModel
    {
        public RoomFilterViewModel()
        {

        }

        public RoomFilterViewModel(string type)
        {
            SelectedType = type;
        }

        public string SelectedType { get; private set; }
    }
}
