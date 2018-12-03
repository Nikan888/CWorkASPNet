using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.ViewModels.Clients;
using Test_Project_Hotel.Models;
using Test_Project_Hotel.ViewModels;
using System.Collections.Generic;

namespace Test_Project_Hotel.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        HotelContext db;

        public ClientsController(HotelContext context)
        {
            db = context;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 5;
            List<ClientViewModel> list = new List<ClientViewModel>();
            var clients = db.Clients;
            foreach (var client in clients)
            {
                list.Add(new ClientViewModel
                {
                    Id = client.ClientID,
                    ClientFIO = client.ClientFIO,
                    ClientPassportData = client.ClientPassportData
                });
            }
            IQueryable<ClientViewModel> filterList = list.AsQueryable();
            var count = filterList.Count();
            var items = filterList.Skip((page - 1) * pageSize).
                Take(pageSize).ToList();
            ClientsListViewModel model = new ClientsListViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Clients = items
            };
            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateClientViewModel model)
        {
            int er = 0;
            if (ModelState.IsValid && (er = db.Clients.Count(p => p.ClientFIO == model.ClientFIO)) == 0)
            {
                Client client = new Client
                {
                    ClientFIO = model.ClientFIO,
                    ClientPassportData = model.ClientPassportData
                };
                await db.Clients.AddAsync(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("ClientFIO", "Record with the same FIO already exists");
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Client client = await db.Clients.FirstOrDefaultAsync(t => t.ClientID == id);
                if (client == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! There is no record in the database with the id passed = " + id
                    };
                    return View("Error", error);
                }
                EditClientViewModel model = new EditClientViewModel
                {
                    Id = client.ClientID,
                    ClientFIO = client.ClientFIO,
                    ClientPassportData = client.ClientPassportData
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
        public async Task<IActionResult> Edit(EditClientViewModel model)
        {
            int er = 0;
            Client client = await db.Clients.FirstOrDefaultAsync(t => t.ClientID == model.Id);
            if (ModelState.IsValid && (model.ClientFIO == client.ClientFIO || (er = db.Clients.Count(p => p.ClientFIO == model.ClientFIO)) == 0))
            {
                if (client == null)
                {
                    ErrorViewModel error = new ErrorViewModel
                    {
                        RequestId = "Error! Empty model was sent"
                    };
                    return View("Error", error);
                }
                client.ClientFIO = model.ClientFIO;
                client.ClientPassportData = model.ClientPassportData;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (er != 0)
                ModelState.AddModelError("ClientFIO", "Record with the same FIO already exists");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Client client = await db.Clients.FirstOrDefaultAsync(t => t.ClientID == id);
            db.Clients.Remove(client);
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
