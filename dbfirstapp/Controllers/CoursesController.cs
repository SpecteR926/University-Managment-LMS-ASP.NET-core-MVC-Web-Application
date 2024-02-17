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
    public class CoursesController : Controller
    {
        private readonly LmsDatabaseContext _context;

        public CoursesController(LmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
               var lmsDatabaseContext = _context.Courses.Include(c => c.Deptment);
            return View(await lmsDatabaseContext.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
            
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Deptment)
                .FirstOrDefaultAsync(m => m.CId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId");
            return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CId,CName,DeptmentId")] Course course)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId", course.DeptmentId);
            return View(course);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId", course.DeptmentId);
            return View(course);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CId,CName,DeptmentId")] Course course)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id != course.CId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.CId))
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
            ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId", course.DeptmentId);
            return View(course);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Deptment)
                .FirstOrDefaultAsync(m => m.CId == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var course = await _context.Courses.FindAsync(id);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        private bool CourseExists(int id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return _context.Courses.Any(e => e.CId == id);
            }
            else
            {
                return false;
            }
        }
    }
}
