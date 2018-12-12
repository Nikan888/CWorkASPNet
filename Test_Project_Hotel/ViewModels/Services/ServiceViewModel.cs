using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Services
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string ClientFIO { get; set; }
        public string RoomType { get; set; }
        public string WorkerFIO { get; set; }

        // Порядок сортировки
        public SortViewModel SortViewModel { get; set; }
    }
}
