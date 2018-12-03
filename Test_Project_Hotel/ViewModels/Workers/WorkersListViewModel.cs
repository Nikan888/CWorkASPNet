using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Workers
{
    public class WorkersListViewModel
    {
        public IEnumerable<WorkerViewModel> Workers { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
