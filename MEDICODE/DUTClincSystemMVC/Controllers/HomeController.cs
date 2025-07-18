using DUTClincSystemMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DUTClincSystemMVC.Controllers
{
    public class HomeController : Controller
    {
        // Create a database instance
        DUTClinicContext db = new DUTClinicContext();

        // function that handles all sessions in the application
        public void UserSession()
        {
            ViewBag.User = Session["User"];
            ViewBag.Student = Session["Student"];
            ViewBag.Doctor = Session["Doctor"];
            ViewBag.Admin = Session["Admin"];
        }

        public ActionResult Index()
        {
            UserSession();
            return View();
        }

        public ActionResult About()
        {
            UserSession();
            return View();
        }

        public ActionResult Contact()
        {
            UserSession();
            return View();
        }

        // Home/Login
        public ActionResult Login()
        {
            UserSession();
            return View();
        }

        // GET: Home/StudentSignUp
        public ActionResult StudentSignUp()
        {
            UserSession();
            return View();
        }

        // POST: Home/StudentSignUp
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentSignUp([Bind(Include = "StudentNumber,Name,Surname,Department,ContactNumber,EmailAddress,StudentPassword")] Student student)
        {
            UserSession();
            ViewBag.ErrorMessage = "";
            if (ModelState.IsValid)
            {
                // verify email address
                if (student.VerifyEmailAddress(student.EmailAddress))
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    Session["Student"] = student;
                    Session["User"] = student;
                    return RedirectToAction("StudentLogin");
                }
                ViewBag.ErrorMessage = "Email address is not a DUT email address";
            }
            return View(student);
        }

        // GET: Home/StudentLogin
        public ActionResult StudentLogin()
        {
            UserSession();
            return View();
        }

        // POST: Home/StudentLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StudentLogin([Bind(Include = "EmailAddress,StudentPassword")] Student student)
        {
            UserSession();
            foreach (Student stud in db.Students.ToList())
            {
                if (stud.EmailAddress == student.EmailAddress && stud.StudentPassword == student.StudentPassword)
                {
                    Session["Student"] = stud;
                    Session["User"] = stud;
                    ViewBag.LoginSuccess = "Welcome back " + stud.StudentNumber;
                    return RedirectToAction("MyAppointments", "Home");
                }
            }
            ViewBag.ErrorMessage = "Invalid username or password!";
            return View(student);
        }

        public ActionResult DoctorLogin()
        {
            UserSession();
            return View();
        }

        [HttpPost]
        public ActionResult DoctorLogin([Bind(Include = "DoctorEmail,DoctorPassword")] Doctor doctor)
        {
            UserSession();
            foreach (Doctor doc in db.Doctors.ToList())
            {
                if (doc.DoctorEmail == doctor.DoctorEmail && doc.DoctorPassword == doctor.DoctorPassword)
                {
                    Session["Doctor"] = doc;
                    Session["User"] = doc;
                    return RedirectToAction("ViewAppointments");
                }
            }
            return View();
        }

        public ActionResult ViewAppointments()
        {
            UserSession();
            var session = Session["Doctor"] as Doctor;

            if (session == null)
                return Redirect("Login");

            return View(db.StudentAppointments.ToList());
        }

        // gets the minimum date that can be selected when making an appointment
        public String getMinimumDate()
        {
            string month = DateTime.Now.Month.ToString();
            month = month.Length == 1 ? "0" + month : month;
            string day = DateTime.Now.Day.ToString();
            day = day.Length == 1 ? "0" + day : day;

            return DateTime.Now.Year + "-" + month + "-" + day;
        }

        // gets the maximum date that can be selected when making an appointment
        public String getMaximumDate(int days)
        {
            string month = DateTime.Now.AddDays(days). Month.ToString();
            month = month.Length == 1 ? "0" + month : month;
            string day = DateTime.Now.AddDays(days).Day.ToString();
            day = day.Length == 1 ? "0" + day : day;

            return DateTime.Now.Year + "-" + month + "-" + day;
        }

        // GET: Home/BookAppointment
        public ActionResult BookAppointment()
        {
            UserSession();
            var user = Session["Student"] as Student;
            if (user != null)
            {
                ViewBag.today = getMinimumDate();
                ViewBag.weeksAfter = getMaximumDate(30);

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        // POST: Home/StudentLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookAppointment([Bind(Include = "AppointmentID,StudentNumber,Date,Time,Campuses,Reason")] StudentAppointment studentAppointment)
        {
            UserSession();
            var student = Session["Student"] as Student;
            
            ViewBag.today = getMinimumDate();
            ViewBag.weeksAfter = getMaximumDate(30);

            if (ModelState.IsValid)
            {
                if (student != null)
                {
                    studentAppointment.StudentNumber = int.Parse(student.StudentNumber);
                    bool ValidTime = studentAppointment.ValidTime(studentAppointment.Time);

                    // check if time is valid (30 minutes interval)
                    if (!ValidTime)
                    {
                        ViewBag.ErrorTime = "Time must be in 30 minutes time intervals e.g(12:00 / 12:30)!" + studentAppointment.Time;
                        return View(studentAppointment);
                    }

                    // If student number doesn't match user then output error
                    if (student.StudentNumber != studentAppointment.StudentNumber.ToString())
                    {
                        ViewBag.ErrorStudentNumber = "Invalid Student Number.";
                        return View(studentAppointment);
                    }

                    // checks if date and time is occupied for bookings
                    foreach (StudentAppointment studApp in db.StudentAppointments.ToList())
                    {
                        // if date and time matches then return error message
                        if (studApp.Date == studentAppointment.Date && studApp.Time == studentAppointment.Time)
                        {
                            ViewBag.ErrorDateTime = "Time Slot is already occupied, choose another time slot!";
                            return View(studentAppointment);
                        }
                            
                    }

                    db.StudentAppointments.Add(studentAppointment);
                    db.SaveChanges();
                    return RedirectToAction("AppointmentBooked");
                }
            }
            return RedirectToAction("Login");
        }

        public ActionResult AppointmentBooked()
        {
            UserSession();
            return View();
        }


        // Home/Advantage
        public ActionResult MyAppointments()
        {
            List<StudentAppointment> myAppointments = new List<StudentAppointment>();

            UserSession();
            Student student = Session["Student"] as Student;

            if (student != null)
            {
                foreach (StudentAppointment appointment in db.StudentAppointments.ToList())
                {
                    if (student.StudentNumber == appointment.StudentNumber.ToString())
                    {
                        myAppointments.Add(appointment);
                    }
                }
                ViewBag.app = myAppointments;
                ViewBag.studNum = student.StudentNumber;
                return View(myAppointments);
            }
            return Redirect("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult CancelAppointment(int? id)
        {
            UserSession();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAppointment appointment = db.StudentAppointments.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("CancelAppointment")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSession();
            StudentAppointment appointment = db.StudentAppointments.Find(id);
            db.StudentAppointments.Remove(appointment);
            db.SaveChanges();
            return RedirectToAction("MyAppointments");
        }
    }
}
