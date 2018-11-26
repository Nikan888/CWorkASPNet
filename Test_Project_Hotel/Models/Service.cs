using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test_Project_Hotel.Models
{
    public class Service
    {
        //ID услуги
        [Key]
        [Display(Name = "Код услуги")]
        public int ServiceID { get; set; }

        //Название услуги
        [Display(Name = "Название услуги")]
        public string ServiceName { get; set; }

        //Описание услуги
        [Display(Name = "Описание услуги")]
        public string ServiceDescription { get; set; }

        //Дата въезда
        [Display(Name = "Дата въезда")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }

        //Дата выезда
        [Display(Name = "Дата выезда")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        //ID клиента
        [Display(Name = "Код клиента")]
        [ForeignKey("Client")]
        public int? ClientID { get; set; }

        //ID сотрудника
        [Display(Name = "Код сотрудника")]
        [ForeignKey("Worker")]
        public int? WorkerID { get; set; }

        //ID номера
        [Display(Name = "Код номера")]
        [ForeignKey("Room")]
        public int? RoomID { get; set; }

        //ссылка по внешнему ключу ClientID на Client
        public virtual Client Client { get; set; }
        //ссылка по внешнему ключу WorkerID на Worker
        public virtual Worker Worker { get; set; }
        //ссылка по внешнему ключу RoomID на Room
        public virtual Room Room { get; set; }
    }
}
