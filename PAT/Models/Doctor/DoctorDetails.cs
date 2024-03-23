using PAT.Models.Admin;
using PAT.Models.Patient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAT.Models.Doctor
{
    public class DoctorDetails
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
        public string DoctorID { get; set; }

        [Required, MaxLength(225), Display(Name = "Password")]
        public string Password { get; set; }

        public bool isApproved { get; set; }
        [ForeignKey("DoctorRoles")]
        public int? RoleID { get; set; }
        public virtual Roles_ DoctorRoles { get; set; }
        public virtual ICollection<DietRecommendation> DoctorID_IN_Diet { get; set; }
     //   public virtual ICollection<TestDetails> TestDetails_IN_Test { get; set; }
    }
}