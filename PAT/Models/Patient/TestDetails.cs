using PAT.Models.Doctor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAT.Models.Patient
{
    public class TestDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("TestDetails_IN_Test")]
        public string DoctorID { get; set; }
        public virtual DoctorDetails TestDetails_IN_Test { get; set; }
        [ForeignKey("TestDetails_IN_PI")]
        public string PatientID { get; set; }
        public virtual PatientDetails TestDetails_IN_PI { get; set; }
        

        [Display(Name = "Tests")]
        public PTests? Test_TODO { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "datetime2")]
        public DateTime? TestPerformedDate { get; set; }
        public TestResults? Test_Result { get; set; }

        public int? TestPrice { get; set; }
        public bool isActive { get; set; }
    }

    public enum TestResults
    {
        Normal_fever, Thypoid, Malaria, Dengue, Positive, Negative
    }

    public enum PTests
    {
        ECG,
        BloodTests,
        X_Ray,
        Covid_19
    }
}