using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.Models
{
    public class Room
    {
        public int RoomID { get; set; }
        public string RoomType { get; set; }
        public int RoomCapacity { get; set; }
        public string RoomDescription { get; set; }
        public decimal RoomPrice { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
