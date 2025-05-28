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
    public class dogAndColorsController : Controller
    {
        private readonly ApppContext _context;

        public dogAndColorsController(ApppContext context)
        {
            _context = context;
        }

        // GET: dogAndColors
        public async Task<IActionResult> Index()
        {
            var apppContext = _context.dogsAndColors.Include(d => d.Color).Include(d => d.dog);
            return View(await apppContext.ToListAsync());
        }

        // GET: dogAndColors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogAndColor = await _context.dogsAndColors
                .Include(d => d.Color)
                .Include(d => d.dog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dogAndColor == null)
            {
                return NotFound();
            }

            return View(dogAndColor);
        }

        // GET: dogAndColors/Create
        public IActionResult Create()
        {
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id");
            ViewData["dogId"] = new SelectList(_context.dogs, "id", "id");
            return View();
        }

        // POST: dogAndColors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ColorId,dogId")] dogAndColor dogAndColor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dogAndColor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", dogAndColor.ColorId);
            ViewData["dogId"] = new SelectList(_context.dogs, "id", "id", dogAndColor.dogId);
            return View(dogAndColor);
        }

        // GET: dogAndColors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogAndColor = await _context.dogsAndColors.FindAsync(id);
            if (dogAndColor == null)
            {
                return NotFound();
            }
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", dogAndColor.ColorId);
            ViewData["dogId"] = new SelectList(_context.dogs, "id", "id", dogAndColor.dogId);
            return View(dogAndColor);
        }

        // POST: dogAndColors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ColorId,dogId")] dogAndColor dogAndColor)
        {
            if (id != dogAndColor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dogAndColor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!dogAndColorExists(dogAndColor.Id))
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
            ViewData["ColorId"] = new SelectList(_context.Colors, "Id", "Id", dogAndColor.ColorId);
            ViewData["dogId"] = new SelectList(_context.dogs, "id", "id", dogAndColor.dogId);
            return View(dogAndColor);
        }

        // GET: dogAndColors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dogAndColor = await _context.dogsAndColors
                .Include(d => d.Color)
                .Include(d => d.dog)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dogAndColor == null)
            {
                return NotFound();
            }

            return View(dogAndColor);
        }

        // POST: dogAndColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dogAndColor = await _context.dogsAndColors.FindAsync(id);
            if (dogAndColor != null)
            {
                _context.dogsAndColors.Remove(dogAndColor);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool dogAndColorExists(int id)
        {
            return _context.dogsAndColors.Any(e => e.Id == id);
        }
    }
}
