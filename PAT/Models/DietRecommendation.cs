using PAT.Models.Doctor;
using PAT.Models.Patient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PAT.Models
{
    public class DietRecommendation
    {

        [Key]
        public int DietID { get; set; }
        [ForeignKey("PatientID_IN_Diet")]
        public string PatientId { get; set; }
        public virtual PatientDetails PatientID_IN_Diet { get; set; }

        [ForeignKey("DoctorID_IN_Diet")]
        public string DoctorId { get; set; }
        public virtual DoctorDetails DoctorID_IN_Diet { get; set; }

        [DisplayName("Diet Duration in Weeks")]
        public DietDuration DietDuration { get; set; }
        [Required]
        [Display(Name = "Diet Contents")]
        public DietContent DietContent { get; set; }
        [Required]
        [Display(Name = "Recommended Exercise")]
        public RecommendedExercise RecommendedExercise { get; set; }
    }

    public enum DietDuration
    {
        One, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Eleven, Twelve
    }

    public enum DietContent
    {
        Fruits_and_vegetables,fish_and_meat,dryfruits,lowfat_dairy_products,milk_egg
    }

    public enum RecommendedExercise
    {
       walking,yoga,meditation,cycling,swimming
    }



}