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

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            List<RoomViewModel> list = new List<RoomViewModel>();
            var rooms = db.Rooms;
            foreach (var room in rooms)
            {
                list.Add(new RoomViewModel
                {
                    Id = room.RoomID,
                    RoomType = room.RoomType,
                    RoomCapacity = room.RoomCapacity,
                    RoomDescription = room.RoomDescription,
                    RoomPrice = room.RoomPrice
                });
            }
            IQueryable<RoomViewModel> filterList = list.AsQueryable();
            var count = filterList.Count();
            var items = filterList.Skip((page - 1) * pageSize).
                Take(pageSize).ToList();
            RoomsListViewModel model = new RoomsListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Rooms = items
            };
            return View(model);
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
