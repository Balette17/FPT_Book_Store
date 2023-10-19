using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;
using FPTBookStore.Data;

namespace FPTBook.Controllers
{
    public class AcceptCatogoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AcceptCatogoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AcceptCatogories
        public async Task<IActionResult> Index()
        {
              return _context.AcceptCatogory != null ? 
                          View(await _context.AcceptCatogory.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.AcceptCatogory'  is null.");
        }

        // GET: AcceptCatogories/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.AcceptCatogory == null)
        //    {
        //        return NotFound();
        //    }

        //    var acceptCatogory = await _context.AcceptCatogory
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (acceptCatogory == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(acceptCatogory);
        //}

        // GET: AcceptCatogories/Create
        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Approve(int id)
        {
            if(_context.AcceptCatogory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TmpCategory' is null");
            }
            var AcceptCatogories = await _context.AcceptCatogory.FindAsync(id);
            await _context.Category.AddAsync(new Category(AcceptCatogories));
            if(AcceptCatogories != null)
            {
                _context.AcceptCatogory.Remove(AcceptCatogories);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        // POST: AcceptCatogories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] AcceptCatogory acceptCatogory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(acceptCatogory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(acceptCatogory);
        }

        // GET: AcceptCatogories/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null || _context.AcceptCatogory == null)
        //    {
        //        return NotFound();
        //    }

        //    var acceptCatogory = await _context.AcceptCatogory.FindAsync(id);
        //    if (acceptCatogory == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(acceptCatogory);
        //}

        //// POST: AcceptCatogories/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] AcceptCatogory acceptCatogory)
        //{
        //    if (id != acceptCatogory.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(acceptCatogory);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!AcceptCatogoryExists(acceptCatogory.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(acceptCatogory);
        //}


        // GET: AcceptCatogories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AcceptCatogory == null)
            {
                return NotFound();
            }

            var acceptCatogory = await _context.AcceptCatogory
                .FirstOrDefaultAsync(m => m.Id == id);
            if (acceptCatogory == null)
            {
                return NotFound();
            }

            return View(acceptCatogory);
        }

        // POST: AcceptCatogories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AcceptCatogory == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AcceptCatogory'  is null.");
            }
            var acceptCatogory = await _context.AcceptCatogory.FindAsync(id);
            if (acceptCatogory != null)
            {
                _context.AcceptCatogory.Remove(acceptCatogory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AcceptCatogoryExists(int id)
        {
          return (_context.AcceptCatogory?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
