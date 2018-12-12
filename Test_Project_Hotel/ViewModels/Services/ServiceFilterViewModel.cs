using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.ViewModels.Services
{
    public class ServiceFilterViewModel
    {
        public string Name { get; set; }
        public SelectList Workers { get; set; }
        public int? SelectedWorker { get; set; }
        //[DataType(DataType.Date)]
        //public DateTime entryDate { get; set; } = DateTime.Now;
        //[DataType(DataType.Date)]
        //public DateTime? departureDate { get; set; } = DateTime.Now;

        public ServiceFilterViewModel()
        {

        }

        public ServiceFilterViewModel(string serviceName, List<Worker> workers, int? worker
            /*DateTime entry, DateTime? departure*/)
        {
            Name = serviceName;
            if (workers.Where(p => p.WorkerFIO == "All").Count() == 0)
                workers.Insert(0, new Worker { WorkerFIO = "All", WorkerID = -1 });
            Workers = new SelectList(workers, "WorkerID", "WorkerFIO", worker);
        }
    }
}
