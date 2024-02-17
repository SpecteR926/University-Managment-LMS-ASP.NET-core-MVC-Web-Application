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
    public class StudentsController : Controller
    {
        private readonly LmsDatabaseContext _context;

        public StudentsController(LmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }
        
    // GET: Students/Details/5
    public async Task<IActionResult> Details(int? id)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {

            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.SId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

        // GET: Students/Create
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

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SId,SName,Dob,SPassword,PhoneNumber,Email")] Student student)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

    // GET: Students/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

    // POST: Students/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("SId,SName,Dob,SPassword,PhoneNumber,Email")] Student student)
    {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            if (id != student.SId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.SId))
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
            return View(student);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

    // GET: Students/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.SId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

    // POST: Students/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

        private bool StudentExists(int id)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            return _context.Students.Any(e => e.SId == id);
        }
        else
        {
            return false;
        }
    }
    }
}
