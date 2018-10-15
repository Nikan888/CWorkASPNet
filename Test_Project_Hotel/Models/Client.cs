using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.Models
{
    public class Client
    {
        public int ClientID { get; set; }
        public string ClientFIO { get; set; }
        public string ClientPassportData { get; set; }

        public ICollection<Service> Services { get; set; }
    }
}
