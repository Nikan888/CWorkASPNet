using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Test_Project_Hotel.ViewModels.Rooms
{
    public enum RoomSortState
    {
        TypeAsc,
        TypeDesc,
        CapacityAsc,
        CapacityDesc,
        DescriptionAsc,
        DescriptionDesc,
        PriceAsc,
        PriceDesc
    }

    public class RoomSortViewModel
    {
        public RoomSortState TypeSort { get; private set; }
        public RoomSortState CapacitySort { get; private set; }
        public RoomSortState DescriptionSort { get; private set; }
        public RoomSortState PriceSort { get; private set; }
        public RoomSortState Current { get; private set; }

        public RoomSortViewModel(RoomSortState sortOrder)
        {
            TypeSort = sortOrder == RoomSortState.TypeAsc ? RoomSortState.TypeDesc : RoomSortState.TypeAsc;
            CapacitySort = sortOrder == RoomSortState.CapacityAsc ? RoomSortState.CapacityDesc : RoomSortState.CapacityAsc;
            DescriptionSort = sortOrder == RoomSortState.DescriptionAsc ? RoomSortState.DescriptionDesc : RoomSortState.DescriptionAsc;
            PriceSort = sortOrder == RoomSortState.PriceAsc ? RoomSortState.PriceDesc : RoomSortState.PriceAsc;
            Current = sortOrder;
        }
    }
}
