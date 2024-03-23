using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAT.Models.Doctor
{
    public class DoctorLogin
    {
        public int ID { get; set; }
        [Required, Display(Name = "Username")]
        public string DoctorID { get; set; }
        [Required, Display(Name = "Password")]
        public string Password { get; set; }
    }
}