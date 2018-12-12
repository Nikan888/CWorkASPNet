using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Services
{
    public enum ServiceSortState
    {
        NameAsc,
        NameDesc,
        ClientFIOAsc,
        ClientFIODesc,
        RoomTypeAsc,
        RoomTypeDesc,
        WorkerFIOAsc,
        WorkerFIODesc
    }

    public class ServiceSortViewModel
    {
        public ServiceSortState NameSort { get; private set; }
        public ServiceSortState ClientFIOSort { get; private set; }
        public ServiceSortState RoomTypeSort { get; private set; }
        public ServiceSortState WorkerFIOSort { get; private set; }
        public ServiceSortState Current { get; private set; }

        public ServiceSortViewModel(ServiceSortState sortOrder)
        {
            NameSort = sortOrder == ServiceSortState.NameAsc ? ServiceSortState.NameDesc : ServiceSortState.NameAsc;
            ClientFIOSort = sortOrder == ServiceSortState.ClientFIOAsc ? ServiceSortState.ClientFIODesc : ServiceSortState.ClientFIOAsc;
            RoomTypeSort = sortOrder == ServiceSortState.RoomTypeAsc ? ServiceSortState.RoomTypeDesc : ServiceSortState.RoomTypeAsc;
            WorkerFIOSort = sortOrder == ServiceSortState.WorkerFIOAsc ? ServiceSortState.WorkerFIODesc : ServiceSortState.WorkerFIOAsc;
            Current = sortOrder;
        }
    }
}
