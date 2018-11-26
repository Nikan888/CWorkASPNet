using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_Project_Hotel.Models
{
    public class Worker
    {
        //Id сотрудника
        [Key]
        [Display(Name = "Код сотрудника")]
        public int WorkerID { get; set; }

        //ФИО сотрудника
        [Display(Name = "ФИО сотрудника")]
        public string WorkerFIO { get; set; }

        //Должность сотрудника
        [Display(Name = "Должность сотрудника")]
        public string WorkerPost { get; set; }

        //Коллекция объектов Service, связанных с моделью
        public virtual ICollection<Service> Services { get; set; }

        public Worker()
        {
            Services = new List<Service>();
        }
    }
}
