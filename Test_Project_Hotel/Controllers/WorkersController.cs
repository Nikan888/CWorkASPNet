using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.ViewModels.Workers;
using Test_Project_Hotel.Models;
using System.Collections.Generic;
using Test_Project_Hotel.ViewModels;

namespace Test_Project_Hotel.Controllers
{
    [Authorize]
    public class WorkersController : Controller
    {
        HotelContext db;

        public WorkersController(HotelContext context)
        {
            db = context;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            List<WorkerViewModel> list = new List<WorkerViewModel>();
            var workers = db.Workers;
            foreach (var worker in workers)
            {
                list.Add(new WorkerViewModel
                {
                    Id = worker.WorkerID,
                    WorkerFIO = worker.WorkerFIO,
                    WorkerPost = worker.WorkerPost
                });
            }
            IQueryable<WorkerViewModel> filterList = list.AsQueryable();
            var count = filterList.Count();
            var items = filterList.Skip((page - 1) * pageSize).
                Take(pageSize).ToList();
            WorkersListViewModel model = new WorkersListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Workers = items
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateWorkerViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Workers.Count(p => p.WorkerFIO == model.WorkerFIO)) == 0)
            {
                Worker worker = new Worker
                {
                    WorkerFIO = model.WorkerFIO,
                    WorkerPost = model.WorkerPost
                };
                await db.Workers.AddAsync(worker);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("WorkerFIO", "Record with the same FIO already exists");
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Worker worker = await db.Workers.FirstOrDefaultAsync(t => t.WorkerID == id);
                if (worker == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! There is no record in the database with the id passed = " + id
                    };
                    return View("Error", error);
                }
                EditWorkerViewModel model = new EditWorkerViewModel
                {
                    Id = worker.WorkerID,
                    WorkerFIO = worker.WorkerFIO,
                    WorkerPost = worker.WorkerPost
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
        public async Task<IActionResult> Edit(EditWorkerViewModel model)
        {
            int er = 0;
            Worker worker = await db.Workers.FirstOrDefaultAsync(t => t.WorkerID == model.Id);
            if (ModelState.IsValid && (model.WorkerFIO == worker.WorkerFIO || (er = db.Workers.Count(p => p.WorkerFIO == model.WorkerFIO)) == 0))
            {
                if (worker == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! Empty model was sent"
                    };
                    return View("Error", error);
                }
                worker.WorkerFIO = model.WorkerFIO;
                worker.WorkerPost = model.WorkerPost;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("WorkerFIO", "Record with the same FIO already exists");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Worker worker = await db.Workers.FirstOrDefaultAsync(t => t.WorkerID == id);
            db.Workers.Remove(worker);
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
