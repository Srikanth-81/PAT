using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PAT.Models.Patient
{
    public class PatientLogin
    {
        public int ID { get; set; }
        [Required, Display(Name = "Username")]
        public string PatientID { get; set; }
        [Required, Display(Name = "Password")]
        public string Password { get; set; }
    }
}