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
    public class StudentsViewController : Controller
    {
        private readonly LmsDatabaseContext _context;
        string data;

        public StudentsViewController(LmsDatabaseContext context)
        {
            _context = context;
           
        }

        // GET: StudentsView
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var info = _context.Students.Where(x => x.Email == HttpContext.Session.GetString("UserSession"));
                return View(info);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }

        }
        public async Task<IActionResult> ViewTeachers()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var data = _context.Teachers.ToList();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
            
        }
        public async Task<IActionResult> ViewExam()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var data = _context.Exams.ToList();
                return View(data);
            }

            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        public async Task<IActionResult> ViewClass()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var data = _context.Schedules.ToList();
                return View(data);
            }

            else
            {
                return RedirectToAction("Index", "Home", null);
            }

          
        }

        public async Task<IActionResult> ViewDep()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                var data = _context.Departments.ToList();
                return View(data);
            }

            else
            {
                return RedirectToAction("Index", "Home", null);
            }

            
        }


        // GET: StudentsView/Details/5
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



        // GET: StudentsView/Edit/5
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
        // POST: StudentsView/Edit/5
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

        // GET: StudentsView/Delete/5
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
