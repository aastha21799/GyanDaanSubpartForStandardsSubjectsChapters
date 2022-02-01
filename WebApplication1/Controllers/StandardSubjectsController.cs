using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain;

namespace WebApplication1.Controllers
{
    public class StandardSubjectsController : Controller
    {
        private readonly Context _context;

        public StandardSubjectsController(Context context)
        {
            _context = context;
        }

        // GET: StandardSubjects
        public async Task<IActionResult> Index()
        {
            var chapterContext = _context.StandardSubjects.Include(s => s.standard).Include(s => s.subject);
            return View(await chapterContext.ToListAsync());
        }

        // GET: StandardSubjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardSubject = await _context.StandardSubjects
                .Include(s => s.standard)
                .Include(s => s.subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardSubject == null)
            {
                return NotFound();
            }

            return View(standardSubject);
        }

        // GET: StandardSubjects/Create
        public IActionResult Create()
        {
            ViewData["standardId"] = new SelectList(_context.Standards, "Id", "Id");
            ViewData["subjectId"] = new SelectList(_context.Subjects, "Id", "Id");
            return View();
        }

        // POST: StandardSubjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,standardId,subjectId")] StandardSubject standardSubject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(standardSubject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["standardId"] = new SelectList(_context.Standards, "Id", "Id", standardSubject.standardId);
            ViewData["subjectId"] = new SelectList(_context.Subjects, "Id", "Id", standardSubject.subjectId);
            return View(standardSubject);
        }

        // GET: StandardSubjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardSubject = await _context.StandardSubjects.FindAsync(id);
            if (standardSubject == null)
            {
                return NotFound();
            }
            ViewData["standardId"] = new SelectList(_context.Standards, "Id", "Id", standardSubject.standardId);
            ViewData["subjectId"] = new SelectList(_context.Subjects, "Id", "Id", standardSubject.subjectId);
            return View(standardSubject);
        }

        // POST: StandardSubjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,standardId,subjectId")] StandardSubject standardSubject)
        {
            if (id != standardSubject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standardSubject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardSubjectExists(standardSubject.Id))
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
            ViewData["standardId"] = new SelectList(_context.Standards, "Id", "Id", standardSubject.standardId);
            ViewData["subjectId"] = new SelectList(_context.Subjects, "Id", "Id", standardSubject.subjectId);
            return View(standardSubject);
        }

        // GET: StandardSubjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standardSubject = await _context.StandardSubjects
                .Include(s => s.standard)
                .Include(s => s.subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standardSubject == null)
            {
                return NotFound();
            }

            return View(standardSubject);
        }

        // POST: StandardSubjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var standardSubject = await _context.StandardSubjects.FindAsync(id);
            _context.StandardSubjects.Remove(standardSubject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandardSubjectExists(int id)
        {
            return _context.StandardSubjects.Any(e => e.Id == id);
        }
    }
}
