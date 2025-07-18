using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DUTClincSystemMVC.Models
{
    public class StudentAppointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AppointmentID { get; set; }

        public int StudentNumber { get; set; }

        public DateTime Date { get; set; }

        public String Time { get; set; }
        public Campus Campuses { get; set; }

        public BookReason Reason { get; set; }

        public enum Campus
        {
            SteveBiko,
            MLSultan,
            Ritson

        }

        public string ChooseCampus()
        {
            string name = "";
            if (Campuses == Campus.SteveBiko)
            {
                name = "Steve Biko";
            }
            else if (Campuses == Campus.MLSultan)
            {
                name = "ML Sultan";

            }
            else if (Campuses == Campus.Ritson)
            {
                name = "Ritson";

            }
            return name;

        }

        public enum BookReason
        {
            Emergency,
            Testing,
            PrimaryCare,
            FamilyPlanning

        }
        public string AppointmentReason()
        {
            string response = "";
            if (Reason == BookReason.Emergency)
            {
                response = "Thank you for reaching out, The Ambulance is on the way";
            }
            else if (Reason == BookReason.Testing)
            {
                response = "Thank you for choosing our Testing services";
            }
            else if (Reason == BookReason.PrimaryCare)
            {
                response = "Thank you for choosing our Primary Care services";

            }
            else if (Reason == BookReason.FamilyPlanning)
            {
                response = "Thank you for choosing our family planning services.";

            }
            return response;

        }

        public bool ValidTime(String time)
        {
            string minutes = time.Substring(3, 2);
            
            if (minutes == "00" || minutes == "30")
            {
                return true;
            }
            return false;
        }
    }
}