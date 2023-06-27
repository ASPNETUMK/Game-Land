using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Game_Land.Data;
using TNAI.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;

namespace Game_Land
{
    public class GryController : Controller
    {
        private readonly ApplicationDbContext _context;
        IWebHostEnvironment _webHostEnvironment;

        public GryController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Gry
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
              return _context.Gry != null ? 
                          View(await _context.Gry.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Gry'  is null.");
        }
        // GET: Gry/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Gry/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Key,Description")] Gry gry)
        {
            List<byte> file1 = new List<byte>();
            string uniqueFileName= Guid.NewGuid().ToString();
            foreach (IFormFile file in Request.Form.Files)
            {
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;

                MemoryStream ms = new MemoryStream();
               file.CopyTo(ms);
              foreach(var i in ms.ToArray() )
                {
                    file1.Add(i);
                }
            }
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");



         
            string filePath = Path.Combine(folderPath, uniqueFileName);
            gry.Url_image = uniqueFileName;
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                fileStream.Write(file1.ToArray());
            }
            gry.Data = new byte[0];

            _context.Add(gry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
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

        public IActionResult end()
        {
            return View();
        }

        

        public async Task<IActionResult> list()       
         {
            return _context.Gry != null ?
                           View(await _context.Gry.ToListAsync()) :
                           Problem("Entity set 'ApplicationDbContext.Gry'  is null.");
        }

        // POST: Gry/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "image");
                string filePath = Path.Combine(folderPath,gry.Url_image);
                    System.IO.File.Delete(filePath);
                _context.Gry.Remove(gry);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GryExists(int id)
        {
          return (_context.Gry?.Any(e => e.id == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> Search(string text = null)
        {
            var Games = new List<Gry>();

            if (string.IsNullOrEmpty(text))
            {
                Games = await _context.Gry.ToListAsync();
            }
            else
            {
                text = text.ToLower();
                Games= await _context.Gry.Where(x => x.Name.ToLower().Contains(text)).ToListAsync();
                 
            }
            return PartialView("Search_Gry", Games);
        }

        public IActionResult Search_list(List<Gry> Game)
        {
            return View(Game);
        }
    }
}
