using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using dbfirstapp.Models;

namespace dbfirstapp.Controllers
{
    public class ExamsController : Controller
    {
        private readonly LmsDatabaseContext _context;

        public ExamsController(LmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Exams
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
            var lmsDatabaseContext = _context.Exams.Include(e => e.Course).Include(e => e.CourseNameNavigation);
            return View(await lmsDatabaseContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Exams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Course)
                .Include(e => e.CourseNameNavigation)
                .FirstOrDefaultAsync(m => m.EId == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Exams/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName");
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName");
            return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Exams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EId,EDate,CourseId,CourseName,RNumber,ETime")] Exam exam)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (ModelState.IsValid)
            {
                _context.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName", exam.CourseId);
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName", exam.CourseName);
            return View(exam);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Exams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName", exam.CourseId);
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName", exam.CourseName);
            return View(exam);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EId,EDate,CourseId,CourseName,RNumber,ETime")] Exam exam)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id != exam.EId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamExists(exam.EId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName", exam.CourseId);
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName", exam.CourseName);
            return View(exam);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Exams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Course)
                .Include(e => e.CourseNameNavigation)
                .FirstOrDefaultAsync(m => m.EId == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        private bool ExamExists(int id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return _context.Exams.Any(e => e.EId == id);
            }
            else
            {
                return false;
            }
        }
    }
}
