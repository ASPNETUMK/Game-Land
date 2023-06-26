using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game_Land.Data;
using Game_Land.Entities;
using TNAI.Model.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Game_Land
{
    public class paysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public paysController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        // GET: pays
        public async Task<IActionResult> Index()
        {
              return _context.pay != null ? 
                          View(await _context.pay.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.pay'  is null.");
        }
        [Authorize(Roles = "Admin")]
        // GET: pays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.pay == null)
            {
                return NotFound();
            }

            var pay = await _context.pay
                .FirstOrDefaultAsync(m => m.id == id);
            if (pay == null)
            {
                return NotFound();
            }

            return View(pay);
        }
        // GET: pays/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult end()
        {
            return View();
        }

        // POST: pays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Last_Name,Number,time")] pay pay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(end));
            }
            return View(pay);
        }
        [Authorize(Roles = "Admin")]
        // GET: pays/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.pay == null)
            {
                return NotFound();
            }

            var pay = await _context.pay.FindAsync(id);
            if (pay == null)
            {
                return NotFound();
            }
            return View(pay);
        }

        // POST: pays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Name,Last_Name,Number,time")] pay pay)
        {
            if (id != pay.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!payExists(pay.id))
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
            return View(pay);
        }
        [Authorize(Roles = "Admin")]
        // GET: pays/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.pay == null)
            {
                return NotFound();
            }

            var pay = await _context.pay
                .FirstOrDefaultAsync(m => m.id == id);
            if (pay == null)
            {
                return NotFound();
            }

            return View(pay);
        }
        [Authorize(Roles = "Admin")]
        // POST: pays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.pay == null)
            {
                return Problem("Entity set 'ApplicationDbContext.pay'  is null.");
            }
            var pay = await _context.pay.FindAsync(id);
            if (pay != null)
            {
                _context.pay.Remove(pay);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool payExists(int id)
        {
          return (_context.pay?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
