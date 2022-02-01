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
    public class StandardsController : Controller
    {
        private readonly Context _context;

        public StandardsController(Context context)
        {
            _context = context;
        }

        // GET: Standards
        public async Task<IActionResult> Index()
        {
            return View(await _context.Standards.ToListAsync());
        }

        // GET: Standards/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standard = await _context.Standards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standard == null)
            {
                return NotFound();
            }

            return View(standard);
        }

        // GET: Standards/Create
        public IActionResult Create(ForStandard model, int? notUsed)
        {
            model.subjectsDrp = _context.Subjects.Select(x => new SelectListItem { Text = x.subjectName, Value = x.Id.ToString() }).ToList();

            return View(model);
        }

        // POST: Standards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ForStandard modelRet)
        {
            //binding
            Standard stdObj = new Standard();
            List<StandardSubject> stdSubRel = new List<StandardSubject>();

            stdObj.standardName = modelRet.standardName;

            if (modelRet.subjectIds != null && modelRet.subjectIds.Length > 0)
            {
                foreach (var subjectId in modelRet.subjectIds)
                {
                    stdSubRel.Add(new StandardSubject { standardId = modelRet.Id, subjectId = subjectId});
                }
            }

            stdObj.standardSubjects = stdSubRel;

            if (ModelState.IsValid)
            {
                _context.Add(stdObj);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Edit", new { id = 2});
        }

        // GET: Standards/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standard = _context.Standards.Include("standardSubjects").FirstOrDefault(x => x.Id == id.Value);
            if (standard == null)
            {
                return NotFound();
            }

            //Filling the subjects in the current standard
            List<int> selectedSubjects = new List<int>();
            standard.standardSubjects.ToList().ForEach(result => selectedSubjects.Add(result.subjectId));

            //Binding the model
            var model = new ForStandard();
            model.Id = standard.Id;
            model.standardName = standard.standardName;
            model.subjectsDrp = _context.Subjects.Select(x => new SelectListItem { Text = x.subjectName, Value = x.Id.ToString() }).ToList();
            model.subjectIds = selectedSubjects.ToArray();

            return View("Create", model);
        }

        // POST: Standards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,standardName")] Standard standard)
        {
            if (id != standard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandardExists(standard.Id))
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
            return View(standard);
        }

        // GET: Standards/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var standard = await _context.Standards
                .FirstOrDefaultAsync(m => m.Id == id);
            if (standard == null)
            {
                return NotFound();
            }

            return View(standard);
        }

        // POST: Standards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var standard = await _context.Standards.FindAsync(id);
            _context.Standards.Remove(standard);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandardExists(int id)
        {
            return _context.Standards.Any(e => e.Id == id);
        }
    }
}
