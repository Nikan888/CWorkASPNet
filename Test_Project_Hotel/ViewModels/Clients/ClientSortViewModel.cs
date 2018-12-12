using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Clients
{
    public enum ClientSortState
    {
        FIOAsc, //по ФИО клиента по возрастанию
        FIODesc, //по ФИО клиента по убыванию
        PassportDataAsc, //по пасспортным данным клиента по возрастанию
        PassportDataDesc //по пасспортным данным клиента по убыванию
    }

    public class ClientSortViewModel
    {
        public ClientSortState FIOSort { get; private set; } //значение для сортировки по ФИО
        public ClientSortState PassportDataSort { get; private set; } //значение для сортировки по пасспортным данным
        public ClientSortState Current { get; private set; } //текущее значение сортировки

        public ClientSortViewModel(ClientSortState sortOrder)
        {
            FIOSort = sortOrder == ClientSortState.FIOAsc ? ClientSortState.FIODesc : ClientSortState.FIOAsc;
            PassportDataSort = sortOrder == ClientSortState.PassportDataAsc ? 
                ClientSortState.PassportDataDesc : ClientSortState.PassportDataAsc;
            Current = sortOrder;
        }
    }
}
