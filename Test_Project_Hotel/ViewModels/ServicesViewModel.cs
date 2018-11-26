using Test_Project_Hotel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Test_Project_Hotel.ViewModels
{
    public class ServicesViewModel
    {
        public IEnumerable<Service> Services { get; set; }

        //Свойство для фильтрации
        public ServiceViewModel ServiceViewModel { get; set; }
        //Свойство для навигации по страницам
        public PageViewModel PageViewModel { get; set; }

        public SelectList ListYears { get; set; }
    }
}
