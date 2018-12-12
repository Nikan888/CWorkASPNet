using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Clients
{
    public class ClientFilterViewModel
    {
        public ClientFilterViewModel()
        {

        }

        public ClientFilterViewModel(string fio)
        {
            SelectedFIO = fio;
        }

        public string SelectedFIO { get; private set; }
    }
}
