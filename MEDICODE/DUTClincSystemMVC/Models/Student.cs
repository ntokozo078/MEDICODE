using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DUTClincSystemMVC.Models
{
    public class Student
    {
        [Key]
        [Required]
        [StringLength(8)]
        [MinLength(8)]
        [MaxLength(8)]

        public string StudentNumber { get; set; }

        public string Name { get; set; }
        [Required]

        public string Surname { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        [StringLength(10)]
        public string ContactNumber { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string StudentPassword { get; set; }

        public bool VerifyEmailAddress(string email)
        {
            // if length is less or greater than 23 then it is not valid
            if (email.Length != 23)
            {
                return false;
            }
            if (email.Substring(0, 8) == StudentNumber && email.Substring(8, 15) == "@dut4life.ac.za")
            {
                return true;
            }
            return false;
        }
    }
}