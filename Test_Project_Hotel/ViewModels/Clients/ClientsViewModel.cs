using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.ViewModels.Clients
{
    public class ClientsViewModel
    {
        //public IEnumerable<ClientViewModel> Clients { get; set; }
        public IEnumerable<Client> Clients { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ClientFilterViewModel ClientFilterViewModel { get; set; }
        public ClientSortViewModel ClientSortViewModel { get; set; }
    }
}
