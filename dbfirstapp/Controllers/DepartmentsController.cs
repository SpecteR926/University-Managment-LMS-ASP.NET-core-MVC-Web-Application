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
    public class DepartmentsController : Controller
    {
        private readonly LmsDatabaseContext _context;

        public DepartmentsController(LmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View(await _context.Departments.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
            
        }

    // GET: Departments/Details/5
    public async Task<IActionResult> Details(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DId,DName")] Department department)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (ModelState.IsValid)
            {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Departments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DId,DName")] Department department)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id != department.DId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DepartmentExists(department.DId))
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
            return View(department);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // GET: Departments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                if (id == null)
            {
                return NotFound();
            }

            var department = await _context.Departments
                .FirstOrDefaultAsync(m => m.DId == id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var department = await _context.Departments.FindAsync(id);
            if (department != null)
            {
                _context.Departments.Remove(department);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        private bool DepartmentExists(int id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return _context.Departments.Any(e => e.DId == id);
            }
            else
            {
                return false;
            }
        }
    }
}
