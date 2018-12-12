using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Workers
{
    public enum WorkerSortState
    {
        FIOAsc,
        FIODesc,
        PostAsc,
        PostDesc
    }

    public class WorkerSortViewModel
    {
        public WorkerSortState FIOSort { get; private set; }
        public WorkerSortState PostSort { get; private set; }
        public WorkerSortState Current { get; private set; }

        public WorkerSortViewModel(WorkerSortState sortOrder)
        {
            FIOSort = sortOrder == WorkerSortState.FIOAsc ? WorkerSortState.FIODesc : WorkerSortState.FIOAsc;
            PostSort = sortOrder == WorkerSortState.PostAsc ?WorkerSortState.PostDesc : WorkerSortState.PostAsc;
            Current = sortOrder;
        }
    }
}
