using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DUTClincSystemMVC.Models
{
    public class Doctor
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Campus: ")]
        public String Campus { get; set; }

        [Required]
        [Display(Name = "Doctor Name: ")]
        public string DoctorName { get; set; }

        [Required]
        [Display(Name = "Doctor Email: ")]
        public string DoctorEmail { get; set; }

        [Required]
        [Display(Name = "Doctor Password: ")]
        public string DoctorPassword { get; set; }

    }
}