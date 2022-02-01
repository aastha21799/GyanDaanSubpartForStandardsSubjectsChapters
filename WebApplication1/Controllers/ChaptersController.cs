using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Domain;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ChaptersController : Controller
    {
        private readonly Context _context;

        public ChaptersController(Context context)
        {
            _context = context;
        }

        // GET: chapters
        public async Task<IActionResult> Index()
        {
            var chapterContext = _context.Chapters.Include(t => t.standardSubject);
            return View(await chapterContext.ToListAsync());
        }

        // GET: chapters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters
                .Include(t => t.standardSubject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // GET: chapters/Create
        public IActionResult Create(ForChapter model, int? notUsed)
        {
            model.standardsDrp = _context.Standards.Select(x => new SelectListItem { Text = x.standardName.ToString(), Value = x.Id.ToString() }).ToList();
            return View(model);
        }

        public JsonResult FillSubjectsDrp(int standardId)
        {

            List<SelectListItem> subjectsDrp = new List<SelectListItem>();
            //_context.StandardSubjects.Include("subject").ToList().ForEach(row =>
            //{
            //    subjectsDrp.Add(new SelectListItem()
            //    {
            //        Text = row.subject.subjectName,
            //        Value = row.subjectId.ToString()
            //    }); ;
            //});
                _context.StandardSubjects.Where(row => row.standardId.Equals(standardId)).Include("subject").ToList().ForEach(row =>
            {
                subjectsDrp.Add(new SelectListItem()
                {
                    Text = row.subject.subjectName,
                    Value = row.subjectId.ToString()
                }); ;
            });
            return Json(subjectsDrp);
        }


        // POST: chapters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ForChapter model)
        {
            Chapter chapterObj = new Chapter();
            if (ModelState.IsValid)
            {
                chapterObj.chapterName = model.chapterName;
                chapterObj.level = model.level;
                var standardSubjectObj = _context.StandardSubjects.Where(row => row.standardId == model.standardId && row.subjectId == model.subjectId).FirstOrDefault();
                chapterObj.standardSubjectId = standardSubjectObj.Id;

                _context.Chapters.Add(chapterObj);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //TBD - return with selected options
            return View();
        }

        // GET: chapters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }
            ViewData["standardSubjectId"] = new SelectList(_context.StandardSubjects, "Id", "Id", chapter.standardSubjectId);
            return View(chapter);
        }

        // POST: chapters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,chapterName,level,standardSubjectId")] Chapter chapter)
        {
            if (id != chapter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chapter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!chapterExists(chapter.Id))
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
            ViewData["standardSubjectId"] = new SelectList(_context.StandardSubjects, "Id", "Id", chapter.standardSubjectId);
            return View(chapter);
        }

        // GET: chapters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var chapter = await _context.Chapters
                .Include(t => t.standardSubject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (chapter == null)
            {
                return NotFound();
            }

            return View(chapter);
        }

        // POST: chapters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var chapter = await _context.Chapters.FindAsync(id);
            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool chapterExists(int id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }
    }
}
