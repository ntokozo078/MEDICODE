using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace DUTClincSystemMVC.Models
{
    public class DUTClinicContext: DbContext
    {

        public DUTClinicContext(): base("DUTClinicContext")
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentAppointment> StudentAppointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

    }
}