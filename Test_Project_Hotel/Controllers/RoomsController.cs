using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.ViewModels.Rooms;
using Test_Project_Hotel.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Test_Project_Hotel.ViewModels;

namespace Test_Project_Hotel.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        HotelContext db;

        public RoomsController(HotelContext context)
        {
            db = context;
        }

        public async Task<IActionResult> Index(string type, int page = 1, RoomSortState sortOrder = RoomSortState.TypeAsc)
        {
            int pageSize = 5;

            //фильтрация
            IQueryable<Room> rooms = db.Rooms;

            if (!string.IsNullOrEmpty(type))
            {
                rooms = rooms.Where(p => p.RoomType.Contains(type));
            }

            // сортировка
            switch (sortOrder)
            {
                case RoomSortState.TypeDesc:
                    rooms = rooms.OrderByDescending(s => s.RoomType);
                    break;
                case RoomSortState.CapacityAsc:
                    rooms = rooms.OrderBy(s => s.RoomCapacity);
                    break;
                case RoomSortState.CapacityDesc:
                    rooms = rooms.OrderByDescending(s => s.RoomCapacity);
                    break;
                case RoomSortState.DescriptionAsc:
                    rooms = rooms.OrderBy(s => s.RoomDescription);
                    break;
                case RoomSortState.DescriptionDesc:
                    rooms = rooms.OrderByDescending(s => s.RoomDescription);
                    break;
                case RoomSortState.PriceAsc:
                    rooms = rooms.OrderBy(s => s.RoomPrice);
                    break;
                case RoomSortState.PriceDesc:
                    rooms = rooms.OrderByDescending(s => s.RoomPrice);
                    break;
                default:
                    rooms = rooms.OrderBy(s => s.RoomType);
                    break;
            }

            // пагинация
            var count = await rooms.CountAsync();
            var items = await rooms.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            RoomsViewModel viewModel = new RoomsViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                RoomSortViewModel = new RoomSortViewModel(sortOrder),
                RoomFilterViewModel = new RoomFilterViewModel(type),
                Rooms = items
            };
            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Rooms.Count(p => p.RoomType == model.RoomType)) == 0)
            {
                Room room = new Room
                {
                    RoomType = model.RoomType,
                    RoomCapacity = model.RoomCapacity,
                    RoomDescription = model.RoomDescription,
                    RoomPrice = model.RoomPrice
                };
                await db.Rooms.AddAsync(room);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("RoomType", "Record with the same type already exists");
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Room room = await db.Rooms.FirstOrDefaultAsync(t => t.RoomID == id);
                if (room == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! There is no record in the database with the id passed = " + id
                    };
                    return View("Error", error);
                }
                EditRoomViewModel model = new EditRoomViewModel
                {
                    Id = room.RoomID,
                    RoomType = room.RoomType,
                    RoomCapacity = room.RoomCapacity,
                    RoomDescription = room.RoomDescription,
                    RoomPrice = room.RoomPrice
                };
                return View(model);
            }
            else
            {
                ErrorViewModel error = new ErrorViewModel { RequestId = "Error! Missing id in request parameters" };
                return View("Error", error);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditRoomViewModel model)
        {
            int er = 0;
            Room room = await db.Rooms.FirstOrDefaultAsync(t => t.RoomID == model.Id);
            if (ModelState.IsValid && (model.RoomType == room.RoomType || (er = db.Rooms.Count(p => p.RoomType == model.RoomType)) == 0))
            {
                if (room == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! Empty model was sent"
                    };
                    return View("Error", error);
                }
                room.RoomType = model.RoomType;
                room.RoomCapacity = model.RoomCapacity;
                room.RoomDescription = model.RoomDescription;
                room.RoomPrice = model.RoomPrice;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("RoomType", "Record with the same type already exists");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Room room = await db.Rooms.FirstOrDefaultAsync(t => t.RoomID == id);
            db.Rooms.Remove(room);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public IActionResult AccessDenied(string returnUrl = null)
        {
            ErrorViewModel error = new ErrorViewModel
            {
                RequestId = "For admin access only"
            };
            return View("Error", error);
        }
    }
}
