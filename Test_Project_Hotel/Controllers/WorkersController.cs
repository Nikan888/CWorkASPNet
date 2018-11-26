using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Test_Project_Hotel.Data;
using Test_Project_Hotel.Infrastructure.Filters;
using Test_Project_Hotel.Models;

namespace Test_Project_Hotel.Controllers
{
    [TypeFilter(typeof(TimingLogAttribute))]
    public class WorkersController : Controller
    {
        private readonly HotelContext _context;

        public WorkersController(HotelContext context)
        {
            _context = context;
        }

        // GET: Workers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Workers.ToListAsync());
        }

        // GET: Workers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Workers
                .SingleOrDefaultAsync(m => m.WorkerID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Workers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Workers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("WorkerID,WorkerFIO,WorkerPost")] Worker room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Workers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Workers.SingleOrDefaultAsync(m => m.WorkerID == id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // POST: Workers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("WorkerID,WorkerFIO,WorkerPost")] Worker room)
        {
            if (id != room.WorkerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(room.WorkerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Workers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Workers
                .SingleOrDefaultAsync(m => m.WorkerID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Workers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Workers.SingleOrDefaultAsync(m => m.WorkerID == id);
            _context.Workers.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkerExists(int id)
        {
            return _context.Workers.Any(e => e.WorkerID == id);
        }
    }
}
