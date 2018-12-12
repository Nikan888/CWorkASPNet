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

        public async Task<IActionResult> Index(string fio, int page = 1, WorkerSortState sortOrder = WorkerSortState.FIOAsc)
        {
            int pageSize = 5;

            //фильтрация
            IQueryable<Worker> workers = db.Workers;

            if (!string.IsNullOrEmpty(fio))
            {
                workers = workers.Where(p => p.WorkerFIO.Contains(fio));
            }

            // сортировка
            switch (sortOrder)
            {
                case WorkerSortState.FIODesc:
                    workers = workers.OrderByDescending(s => s.WorkerFIO);
                    break;
                case WorkerSortState.PostAsc:
                    workers = workers.OrderBy(s => s.WorkerPost);
                    break;
                case WorkerSortState.PostDesc:
                    workers = workers.OrderByDescending(s => s.WorkerPost);
                    break;
                default:
                    workers = workers.OrderBy(s => s.WorkerFIO);
                    break;
            }

            // пагинация
            var count = await workers.CountAsync();
            var items = await workers.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // формируем модель представления
            WorkersViewModel viewModel = new WorkersViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                WorkerSortViewModel = new WorkerSortViewModel(sortOrder),
                WorkerFilterViewModel = new WorkerFilterViewModel(fio),
                Workers = items
            };
            return View(viewModel);
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
