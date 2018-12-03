using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Clients
{
    public class ClientsListViewModel
    {
        public IEnumerable<ClientViewModel> Clients { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
