using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class dogsController : Controller
    {
        private readonly ApppContext _context;

        public dogsController(ApppContext context)
        {
            _context = context;
        }

        // GET: dogs
        public async Task<IActionResult> Index()
        {
            var apppContext = _context.dogs.Include(d => d.Owner);
            return View(await apppContext.ToListAsync());
        }

        // GET: dogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.dogs
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.id == id);
            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // GET: dogs/Create
        public IActionResult Create()
        {
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id");
            return View();
        }

        // POST: dogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,OwnerId")] dog dog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", dog.OwnerId);
            return View(dog);
        }

        // GET: dogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.dogs.FindAsync(id);
            if (dog == null)
            {
                return NotFound();
            }
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", dog.OwnerId);
            return View(dog);
        }

        // POST: dogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("id,name,OwnerId")] dog dog)
        {
            if (id != dog.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!dogExists(dog.id))
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
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", dog.OwnerId);
            return View(dog);
        }

        // GET: dogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dog = await _context.dogs
                .Include(d => d.Owner)
                .FirstOrDefaultAsync(m => m.id == id);
            if (dog == null)
            {
                return NotFound();
            }

            return View(dog);
        }

        // POST: dogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var dog = await _context.dogs.FindAsync(id);
            if (dog != null)
            {
                _context.dogs.Remove(dog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool dogExists(int? id)
        {
            return _context.dogs.Any(e => e.id == id);
        }
    }
}
