using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DUTClincSystemMVC.Models;

namespace DUTClincSystemMVC.Controllers
{
    public class StudentAppointmentsController : Controller
    {
        private DUTClinicContext db = new DUTClinicContext();

        // GET: StudentAppointments
        public ActionResult Index()
        {
            return View(db.StudentAppointments.ToList());
        }

        // GET: StudentAppointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAppointment studentAppointment = db.StudentAppointments.Find(id);
            if (studentAppointment == null)
            {
                return HttpNotFound();
            }
            return View(studentAppointment);
        }

        // GET: StudentAppointments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentAppointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppointmentID,StudentNumber,Date,Time,Campuses,Reason")] StudentAppointment studentAppointment)
        {
            if (ModelState.IsValid)
            {
                db.StudentAppointments.Add(studentAppointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(studentAppointment);
        }

        public ActionResult ViewAppointments(string sortOrder)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date" : "";
            var appointments = from a in db.StudentAppointments
                               select a;

            switch (sortOrder)
            {
                case "date":
                    appointments = appointments.OrderBy(a => a.Date).ThenBy(a => a.Time);
                    break;
                default:
                    appointments = appointments.OrderBy(a => a.Date).ThenBy(a => a.Time);
                    break;
            }

            return View(appointments.ToList());
        }

        // GET: StudentAppointments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAppointment studentAppointment = db.StudentAppointments.Find(id);
            if (studentAppointment == null)
            {
                return HttpNotFound();
            }
            return View(studentAppointment);
        }

        // POST: StudentAppointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppointmentID,StudentNumber,Date,Time,Campuses,Reason")] StudentAppointment studentAppointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentAppointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(studentAppointment);
        }

        // GET: StudentAppointments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentAppointment studentAppointment = db.StudentAppointments.Find(id);
            if (studentAppointment == null)
            {
                return HttpNotFound();
            }
            return View(studentAppointment);
        }

        // POST: StudentAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentAppointment studentAppointment = db.StudentAppointments.Find(id);
            db.StudentAppointments.Remove(studentAppointment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
