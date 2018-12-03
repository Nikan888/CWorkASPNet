using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Services
{
    public class EditServiceViewModel
    {
        public int Id { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDescription { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int? ClientID { get; set; }
        public int? WorkerID { get; set; }
        public int? RoomID { get; set; }
    }
}
