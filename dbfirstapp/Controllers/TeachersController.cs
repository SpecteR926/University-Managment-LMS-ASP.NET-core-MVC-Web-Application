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
    public class TeachersController : Controller
    {

        private readonly LmsDatabaseContext _context;

        public TeachersController(LmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Teachers
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return View(await _context.Teachers.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
            
        }
         
    // GET: Teachers/Details/5
    public async Task<IActionResult> Details(int? id)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {

            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.TId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

    // GET: Teachers/Create
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
        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TId,TName,Dob,TPassword,PhoneNumber,Email")] Teacher teacher)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {

            if (ModelState.IsValid)
            {
                _context.Add(teacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {

            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return View(teacher);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TId,TName,Dob,TPassword,PhoneNumber,Email")] Teacher teacher)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            if (id != teacher.TId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherExists(teacher.TId))
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
            return View(teacher);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _context.Teachers
                .FirstOrDefaultAsync(m => m.TId == id);
            if (teacher == null)
            {
                return NotFound();
            }

            return View(teacher);
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
        if (HttpContext.Session.GetString("UserSession") != null)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher != null)
            {
                _context.Teachers.Remove(teacher);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return RedirectToAction("Index", "Home", null);
        }
    }

        private bool TeacherExists(int id)
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                return _context.Teachers.Any(e => e.TId == id);
            }
            else
            {
                return false;
            }
        }
    }
}
