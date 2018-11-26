using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_Project_Hotel.Models
{
    public class Client
    {
        //Id клиента
        [Key]
        [Display(Name = "Код клиента")]
        public int ClientID { get; set; }

        //ФИО клиента
        [Display(Name = "ФИО клиента")]
        public string ClientFIO { get; set; }

        //Пасспортные данные клиента
        [Display(Name = "Пасспортные данные клиента")]
        public string ClientPassportData { get; set; }

        //Коллекция объектов Service, связанных с моделью
        public virtual ICollection<Service> Services { get; set; }

        public Client()
        {
            Services = new List<Service>();
        }
    }
}
