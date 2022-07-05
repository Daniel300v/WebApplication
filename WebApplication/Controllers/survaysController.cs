using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication.Data;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class survaysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public survaysController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: survays
        public async Task<IActionResult> Index()
        {
            return View(await _context.survay.ToListAsync());
        }

        // GET: survays/ShowSearchForm

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // PoST: survays/ShowSearchResults

        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _context.survay.Where(J =>J.feedback.Contains(SearchPhrase) ).ToListAsync());
        }

        // GET: survays/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survay = await _context.survay
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survay == null)
            {
                return NotFound();
            }

            return View(survay);
        }

        // GET: survays/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: survays/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PayRateing,SatisfactionRating,feedback")] survay survay)
        {
            if (ModelState.IsValid)
            {
                _context.Add(survay);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(survay);
        }

        // GET: survays/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survay = await _context.survay.FindAsync(id);
            if (survay == null)
            {
                return NotFound();
            }
            return View(survay);
        }

        // POST: survays/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PayRateing,SatisfactionRating,feedback")] survay survay)
        {
            if (id != survay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(survay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!survayExists(survay.Id))
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
            return View(survay);
        }

        // GET: survays/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var survay = await _context.survay
                .FirstOrDefaultAsync(m => m.Id == id);
            if (survay == null)
            {
                return NotFound();
            }

            return View(survay);
        }

        // POST: survays/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var survay = await _context.survay.FindAsync(id);
            _context.survay.Remove(survay);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool survayExists(int id)
        {
            return _context.survay.Any(e => e.Id == id);
        }
    }
}
