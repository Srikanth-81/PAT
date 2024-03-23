using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PAT.Models.Clerk
{
    public class ClerkLogin
    {
        public int ID { get; set; }
        [Required, Display(Name = "Username")]
        public string ClerkID { get; set; }
        [Required, Display(Name = "Password")]
        public string Password { get; set; }
    }
}