using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Test_Project_Hotel.Models
{
    public class Room
    {
        //Id номера
        [Key]
        [Display(Name = "Код номера")]
        public int RoomID { get; set; }

        //Тип номера
        [Display(Name = "Тип номера")]
        public string RoomType { get; set; }

        //Вместимость номера
        [Display(Name = "Вместимость номера")]
        public int RoomCapacity { get; set; }

        //Описание номера
        [Display(Name = "Описание номера")]
        public string RoomDescription { get; set; }

        //Стоимость номера
        [Display(Name = "Стоимость номера")]
        public decimal RoomPrice { get; set; }

        //Коллекция объектов Service, связанных с моделью
        public virtual ICollection<Service> Services { get; set; }

        public Room()
        {
            Services = new List<Service>();
        }
    }
}
