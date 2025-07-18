using DUTClincSystemMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DUTClincSystemMVC.Controllers
{
    public class AdminController : Controller
    {
        DUTClinicContext db = new DUTClinicContext();

        // GET: Admin
        public ActionResult Index()
        {

            if (Session["Admin"] != null)
            {
                ViewBag.NumberOfStudents = db.Students.ToList().Count;
                ViewBag.NumberOfAppointments = db.StudentAppointments.ToList().Count;
                ViewBag.NumberOfDoctors = db.Doctors.ToList().Count;

                return View();
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ViewStudents()
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Students");
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ViewAppointments()
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "StudentAppointments");
            }
            return RedirectToAction("Login", "Home");
        }

        public ActionResult ViewDoctors()
        {
            if (Session["Admin"] != null)
            {
                return RedirectToAction("Index", "Doctors");

            }
            return RedirectToAction("Login", "Home");

        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            if (email == "ntokozo@dut4life.ac.za" && password == "Ntokozo@02")
            {
                Session["Admin"] = "Admin";
                return RedirectToAction("Index");
            }
            ViewBag.ErrorMessage = "Invalid Email or Password!";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}