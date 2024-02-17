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
    public class SchedulesController : Controller
    {
        private readonly LmsDatabaseContext _context;

        public SchedulesController(LmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Schedules
        public async Task<IActionResult> Index()
        {
            var lmsDatabaseContext = _context.Schedules.Include(s => s.Course).Include(s => s.CourseNameNavigation).Include(s => s.Deptment).Include(s => s.Teacher);
            return View(await lmsDatabaseContext.ToListAsync());
        }

        // GET: Schedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.CourseNameNavigation)
                .Include(s => s.Deptment)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.EId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName");
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName");
            ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TId", "TId");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EId,EDate,CourseId,DeptmentId,TeacherId,CourseName,RNumber,ETime")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName", schedule.CourseId);
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName", schedule.CourseName);
            ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId", schedule.DeptmentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TId", "TId", schedule.TeacherId);
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName", schedule.CourseId);
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName", schedule.CourseName);
            ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId", schedule.DeptmentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TId", "TId", schedule.TeacherId);
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EId,EDate,CourseId,DeptmentId,TeacherId,CourseName,RNumber,ETime")] Schedule schedule)
        {
            if (id != schedule.EId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.EId))
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
            ViewData["CourseId"] = new SelectList(_context.Courses, "CId", "CName", schedule.CourseId);
            ViewData["CourseName"] = new SelectList(_context.Courses, "CName", "CName", schedule.CourseName);
            ViewData["DeptmentId"] = new SelectList(_context.Departments, "DId", "DId", schedule.DeptmentId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "TId", "TId", schedule.TeacherId);
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedules
                .Include(s => s.Course)
                .Include(s => s.CourseNameNavigation)
                .Include(s => s.Deptment)
                .Include(s => s.Teacher)
                .FirstOrDefaultAsync(m => m.EId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(int id)
        {
            return _context.Schedules.Any(e => e.EId == id);
        }
    }
}
