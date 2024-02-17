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
    public class MasterController : Controller
    {
        private readonly LmsDatabaseContext _context;

        public MasterController(LmsDatabaseContext context)
        {
            _context = context;
        }

        // GET: Master
        public async Task<IActionResult> Index()
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

    }
}
