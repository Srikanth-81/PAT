using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PAT.Models.Admin
{
    public class AdminLogin
    {
        public int Id { get; set; }
        [Required, Display(Name = "Username")]
        public string AdminID { get; set; }
        [Required, Display(Name = "Password")]
        public string Password { get; set; }
    }
}