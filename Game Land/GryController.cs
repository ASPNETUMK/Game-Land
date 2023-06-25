using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game_Land.Data;
using TNAI.Model.Entities;

namespace Game_Land
{
    public class GryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Gry
        public async Task<IActionResult> Index()
        {
              return _context.Gry != null ? 
                          View(await _context.Gry.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Gry'  is null.");
        }

        // GET: Gry/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Gry == null)
            {
                return NotFound();
            }

            var gry = await _context.Gry
                .FirstOrDefaultAsync(m => m.id == id);
            if (gry == null)
            {
                return NotFound();
            }

            return View(gry);
        }

        // GET: Gry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Key,Url_image")] Gry gry)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gry);
        }

        // GET: Gry/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Gry == null)
            {
                return NotFound();
            }

            var gry = await _context.Gry.FindAsync(id);
            if (gry == null)
            {
                return NotFound();
            }
            return View(gry);
        }

        // POST: Gry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Key,Url_image")] Gry gry)
        {
            if (id != gry.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GryExists(gry.id))
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
            return View(gry);
        }

        // GET: Gry/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Gry == null)
            {
                return NotFound();
            }

            var gry = await _context.Gry
                .FirstOrDefaultAsync(m => m.id == id);
            if (gry == null)
            {
                return NotFound();
            }

            return View(gry);
        }

        // POST: Gry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Gry == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Gry'  is null.");
            }
            var gry = await _context.Gry.FindAsync(id);
            if (gry != null)
            {
                _context.Gry.Remove(gry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GryExists(int id)
        {
          return (_context.Gry?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
