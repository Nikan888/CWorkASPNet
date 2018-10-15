using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.Models
{
    public class Worker
    {
        public int WorkerID { get; set; }
        public string WorkerFIO { get; set; }
        public string WorkerPost { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
