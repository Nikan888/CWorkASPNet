using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }

        public int? ClientID { get; set; }
        public int? WorkerID { get; set; }
        public int? RoomID { get; set; }

        public virtual Client Client { get; set; }
        public virtual Worker Worker { get; set; }
        public virtual Room Room { get; set; }
    }
}
