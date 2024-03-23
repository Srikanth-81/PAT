using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PAT.Models.Admin;
using PAT.Models.Doctor;

namespace PAT.Models.Patient
{
    public class PatientDetails
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

        [Key, Required, MaxLength(225), Display(Name = "Username"), Index("ad", IsUnique = true)]
        public string PatientID { get; set; }

        [Required, MaxLength(225), Display(Name = "Password")]
        public string Password { get; set; }

        public bool isApproved { get; set; }
        [ForeignKey("PatientRoles")]
        public int? RoleID { get; set; }
        public virtual Roles_ PatientRoles { get; set; }
        public virtual ICollection<DietRecommendation> PatientID_IN_Diet { get; set; }
        [ForeignKey("DoctorId_IN_PD")]
        public string DoctorID { get; set; }
        public virtual DoctorDetails DoctorId_IN_PD { get; set; }
        public virtual ICollection<TestDetails> TestDetails_IN_PI { get; set; }
        public bool TestRequest { get; set; }
        public Illness Illnes { get; set; }
        public enum Illness
        {
            Fever_and_cough, Headache,stomachpain,cold_cough,chest_pain,fracture
        }
    }
}
