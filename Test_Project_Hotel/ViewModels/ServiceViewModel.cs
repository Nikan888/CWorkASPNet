using System;
using System.ComponentModel.DataAnnotations;

namespace Test_Project_Hotel.ViewModels
{
    public class ServiceViewModel
    {
        //ID услуги
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
        //ФИО клиента
        [Display(Name = "Клиент")]
        public string ClientFIO { get; set; }
        //Тип номера
        [Display(Name = "Номер")]
        public string RoomType { get; set; }
        //ФИО сотрудника
        [Display(Name = "Сотрудник")]
        public string WorkerFIO { get; set; }
        // Порядок сортировки
        public SortViewModel SortViewModel { get; set; }
    }
}
