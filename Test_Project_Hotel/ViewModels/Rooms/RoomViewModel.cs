﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Rooms
{
    public class RoomViewModel
    {
        public int Id { get; set; }
        public string RoomType { get; set; }
        public int RoomCapacity { get; set; }
        public string RoomDescription { get; set; }
        public decimal RoomPrice { get; set; }
    }
}
