using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PAT.Models.Admin;

namespace PAT.Models.Clerk
{
    public class ClerkDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required, MaxLength(225), Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required, MaxLength(225), Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required, Display(Name = "Age")]
        public short Age { get; set; }

        [Display(Name = "Gender")]
        public GenderType Gender { get; set; }

        [Required, Display(Name = "Contact Number"), RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Mobile Number.")]
        public string ContectNumber { get; set; }

        [Key, Required, MaxLength(225), Display(Name = "Username")]
        public string ClerkID { get; set; }

        [Required, MaxLength(225), Display(Name = "Password")]
        public string Password { get; set; }

        public bool isApproved { get; set; }
        [ForeignKey("ClerkRoles")]
        public int? RoleID { get; set; }
        public virtual Roles_ ClerkRoles { get; set; }
    }
}
