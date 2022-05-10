#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GroupCoursework.Models;

namespace GroupCoursework.Controllers
{
    public class DVDCategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public DVDCategoriesController(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.DVDCategory.ToListAsync());
        }


        // GET: Method to get the DVDCategories Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCategory = await _context.DVDCategory
                .FirstOrDefaultAsync(m => m.CategoryNumber == id);
            if (dVDCategory == null)
            {
                return NotFound();
            }

            return View(dVDCategory);
        }


        public IActionResult Create()
        {
            return View();
        }

        // Post Method to create new entry in DVDCategories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryNumber,CategoryDescription,AgeRestricted")] DVDCategory dVDCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dVDCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dVDCategory);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCategory = await _context.DVDCategory.FindAsync(id);
            if (dVDCategory == null)
            {
                return NotFound();
            }
            return View(dVDCategory);
        }

        // Post Method to edit new entry in DVDCategories
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryNumber,CategoryDescription,AgeRestricted")] DVDCategory dVDCategory)
        {
            if (id != dVDCategory.CategoryNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dVDCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DVDCategoryExists(dVDCategory.CategoryNumber))
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
            return View(dVDCategory);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dVDCategory = await _context.DVDCategory
                .FirstOrDefaultAsync(m => m.CategoryNumber == id);
            if (dVDCategory == null)
            {
                return NotFound();
            }

            return View(dVDCategory);
        }

        // Post Method to delete new entry in DVDCategories
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dVDCategory = await _context.DVDCategory.FindAsync(id);
            _context.DVDCategory.Remove(dVDCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DVDCategoryExists(int id)
        {
            return _context.DVDCategory.Any(e => e.CategoryNumber == id);
        }
    }
}
