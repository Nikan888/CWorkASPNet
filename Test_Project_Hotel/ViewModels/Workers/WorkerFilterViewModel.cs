using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Workers
{
    public class WorkerFilterViewModel
    {
        public WorkerFilterViewModel()
        {

        }

        public WorkerFilterViewModel(string fio)
        {
            SelectedFIO = fio;
        }

        public string SelectedFIO { get; private set; }
    }
}
