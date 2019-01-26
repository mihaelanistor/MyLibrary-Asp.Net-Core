using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLibrary.Data;
using MyLibrary.Models;

namespace MyLibrary.Controllers
{
    public class BorrowedBooksListsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BorrowedBooksListsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: BorrowedBooksLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.BorrowedBooksList.ToListAsync());
        }

        // GET: BorrowedBooksLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBooksList = await _context.BorrowedBooksList
                .FirstOrDefaultAsync(m => m.BorrowedBooksListId == id);
            if (borrowedBooksList == null)
            {
                return NotFound();
            }

            return View(borrowedBooksList);
        }

        // GET: BorrowedBooksLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BorrowedBooksLists/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BorrowedBooksListId,UserId,BookId")] BorrowedBooksList borrowedBooksList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(borrowedBooksList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(borrowedBooksList);
        }

        // GET: BorrowedBooksLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBooksList = await _context.BorrowedBooksList.FindAsync(id);
            if (borrowedBooksList == null)
            {
                return NotFound();
            }
            return View(borrowedBooksList);
        }

        // POST: BorrowedBooksLists/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BorrowedBooksListId,UserId,BookId")] BorrowedBooksList borrowedBooksList)
        {
            if (id != borrowedBooksList.BorrowedBooksListId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(borrowedBooksList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BorrowedBooksListExists(borrowedBooksList.BorrowedBooksListId))
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
            return View(borrowedBooksList);
        }

        // GET: BorrowedBooksLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var borrowedBooksList = await _context.BorrowedBooksList
                .FirstOrDefaultAsync(m => m.BorrowedBooksListId == id);
            if (borrowedBooksList == null)
            {
                return NotFound();
            }

            return View(borrowedBooksList);
        }

        // POST: BorrowedBooksLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var borrowedBooksList = await _context.BorrowedBooksList.FindAsync(id);
            _context.BorrowedBooksList.Remove(borrowedBooksList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BorrowedBooksListExists(int id)
        {
            return _context.BorrowedBooksList.Any(e => e.BorrowedBooksListId == id);
        }
    }
}
