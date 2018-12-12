using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.ViewModels.Workers
{
    public class WorkersViewModel
    {
        //public IEnumerable<WorkerViewModel> Workers { get; set; }
        public IEnumerable<Worker> Workers { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public WorkerFilterViewModel WorkerFilterViewModel { get; set; }
        public WorkerSortViewModel WorkerSortViewModel { get; set; }
    }
}
