using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Services
{
    public class ServicesListViewModel
    {
        public IEnumerable<ServiceViewModel> Services { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
