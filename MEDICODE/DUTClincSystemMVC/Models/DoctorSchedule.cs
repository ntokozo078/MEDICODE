using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DUTClincSystemMVC.Models
{
    public class DoctorSchedule
    {
        [Key]
        public int ScheduleID { get; set; }

        public int DoctorID { get; set; }

        public string DayOfWeek { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int IntervalMinutes { get; set; }


        [ForeignKey("AppointmentID")]
        public int AppointmentID { get; set; }
    }
}