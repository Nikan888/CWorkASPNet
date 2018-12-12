using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.ViewModels.Services
{
    public class ServicesViewModel
    {
        public IEnumerable<Service> Services { get; set; }
        //public IEnumerable<ServiceViewModel> Services { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public ServiceFilterViewModel ServiceFilterViewModel { get; set; }
        public ServiceSortViewModel ServiceSortViewModel { get; set; }
    }
}
