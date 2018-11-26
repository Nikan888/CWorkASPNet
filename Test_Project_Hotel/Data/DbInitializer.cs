using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.Data
{
    public static class DbInitializer
    {
        public static void Initialize(HotelContext db)
        {
            db.Database.EnsureCreated();

            // Проверка занесены ли клиенты
            if (db.Clients.Any())
            {
                return;   // База данных инициализирована
            }

            int clients_number = 35;
            int rooms_number = 35;
            int workers_number = 35;
            int services_number = 300;
            string clientFio;
            string clientPassportData;
            string roomType;
            int roomCapacity;
            string roomDescription;
            decimal roomPrice;
            string workerFio;
            string workerPost;
            string serviceName;
            string serviceDescription;

            Random randObj = new Random(29);

            //Заполнение таблицы клиентов
            string[] clientFio_voc = { "Белов_", "Логвинов_", "Зыков_", "Ульянова_", "Панов_" };//словарь фамилий клиентов
            string[] clientPassportData_voc = { "HB4521857", "HB3652987", "HB857436", "HB3456789", "HB987654", "HB745362", "HB4567852" };//словарь пасспортных данных клиентов
            int count_clientFio_voc = clientFio_voc.GetLength(0);
            int count_clientPassportData_voc = clientPassportData_voc.GetLength(0);
            for (int clientID = 1; clientID <= clients_number; clientID++)
            {
                clientFio = clientFio_voc[randObj.Next(count_clientFio_voc)] + clientID.ToString();
                clientPassportData = clientPassportData_voc[randObj.Next(count_clientPassportData_voc)];
                db.Clients.Add(new Client { ClientFIO = clientFio, ClientPassportData = clientPassportData });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            //Заполнение таблицы номеров
            string[] roomType_voc = { "single_", "double_", "twin_", "triple_", "deluxe_", "duplex_" };
            string[] roomDescription_voc = { "desc1", "desc2", "desc3", "desc4", "desc5", "desc6", "desc7", };
            int count_roomType_voc = roomType_voc.GetLength(0);
            int count_roomDescription_voc = roomDescription_voc.GetLength(0);
            for (int roomID = 1; roomID <= rooms_number; roomID++)
            {
                roomType = roomType_voc[randObj.Next(count_roomType_voc)] + roomID.ToString();
                roomCapacity = randObj.Next(1, 10);
                roomDescription = roomDescription_voc[randObj.Next(count_roomDescription_voc)];
                roomPrice = randObj.Next(1, 1000);
                db.Rooms.Add(new Room
                {
                    RoomType = roomType,
                    RoomCapacity = roomCapacity,
                    RoomDescription = roomDescription,
                    RoomPrice = roomPrice
                });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            //Заполнение таблицы сотрудников
            string[] workerFio_voc = { "Горбунов_", "Моисеев_", "Громов_", "Васильева_", "Зайцева_" };//словарь фамилий сотрудников
            string[] workerPost_voc = { "администратор", "бармен", "повар",
                "швейцар", "портье", "служба охраны", "инженерная служба" };//словарь должностей
            int count_workerFio_voc = workerFio_voc.GetLength(0);
            int count_workerPost_voc = workerPost_voc.GetLength(0);
            for (int workerID = 1; workerID <= clients_number; workerID++)
            {
                workerFio = workerFio_voc[randObj.Next(count_workerFio_voc)] + workerID.ToString();
                workerPost = clientPassportData_voc[randObj.Next(count_workerPost_voc)];
                db.Workers.Add(new Worker { WorkerFIO = workerFio, WorkerPost = workerPost });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();

            //Заполнение таблицы услуг
            string[] serviceName_voc = { "service1_", "service2_", "service3_", "service4_", "service5_" };
            string[] serviceDescription_voc = { "serDesc1", "serDesc2", "serDesc3", "serDesc4", "serDesc5",
                "serDesc6", "serDesc7" };
            int count_serviceName_voc = serviceName_voc.GetLength(0);
            int count_serviceDescription_voc = serviceDescription_voc.GetLength(0);
            for (int serviceID = 1; serviceID <= services_number; serviceID++)
            {
                serviceName = serviceName_voc[randObj.Next(count_serviceName_voc)] + serviceID.ToString();
                serviceDescription = serviceDescription_voc[randObj.Next(count_serviceDescription_voc)];
                int clientID = randObj.Next(1, clients_number - 1);
                int workerID = randObj.Next(1, workers_number - 1);
                int roomID = randObj.Next(1, rooms_number - 1);
                DateTime today = DateTime.Now.Date;
                DateTime entryDate = today.AddDays(-serviceID);
                DateTime departureDate = today.AddDays(serviceID);
                db.Services.Add(new Service
                {
                    ServiceName = serviceName,
                    ServiceDescription = serviceDescription,
                    EntryDate = entryDate,
                    DepartureDate = departureDate,
                    ClientID = clientID,
                    WorkerID = workerID,
                    RoomID = roomID
                });
            }
            //сохранение изменений в базу данных, связанную с объектом контекста
            db.SaveChanges();
        }
    }
}
