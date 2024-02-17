using dbfirstapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace dbfirstapp.Controllers
{
    public class HomeController : Controller
    {

        private readonly LmsDatabaseContext context;

        public HomeController(LmsDatabaseContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
            }

            return View();
        }

        public IActionResult StudentLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StudentLogin(Student user)
        {
            var myUser = context.Students.Where(x => x.Email == user.Email && x.SPassword == user.SPassword).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Index", "StudentsView", null);
            }
            else
            {
                ViewBag.Message = "Incorrect Email or Password";
            }
            return View();
        }
        public IActionResult StaffLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StaffLogin(Admin user)
        {
            var myUser = context.Admins.Where(x => x.AId == user.AId && x.APassword == user.APassword).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetInt32("UserSession", myUser.AId);
                return RedirectToAction("Index", "Master", null);
            }
            else
            {
                ViewBag.Message = "Incorrect Email or Password";
            }
            return View();
        }

        public IActionResult TeacherLogin()
        {

            return View();
        }

        [HttpPost]
        public IActionResult TeacherLogin(Teacher user)
        {
            var myUser = context.Teachers.Where(x => x.Email == user.Email && x.TPassword == user.TPassword).FirstOrDefault();
            if (myUser != null)
            {
                HttpContext.Session.SetString("UserSession", myUser.Email);
                return RedirectToAction("Index", "TeachersView", null);
            }
            else
            {
                ViewBag.Message = "Incorrect Email or Password";
            }
            return View();
        }

        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("UserSession") != null)
            {
                HttpContext.Session.Remove("UserSession");
                return RedirectToAction("Index");
            }
            return View("Index");
        }



        public IActionResult Contact_Us()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
