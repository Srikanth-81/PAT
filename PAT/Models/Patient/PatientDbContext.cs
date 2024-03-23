using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PAT.Models.Patient
{

    public class PatientDbContext : DbContext
    {
        public PatientDbContext() : base("PatientTracker")
        {

        }
        public DbSet<PatientDetails> Patients { get; set; }
        public DbSet<PatientLogin> PLogins { get; set; }
        public DbSet<TestDetails> Tests { get; set; }

        public System.Data.Entity.DbSet<PAT.Models.Doctor.DoctorDetails> DoctorDetails { get; set; }

        public System.Data.Entity.DbSet<PAT.Models.Roles_> Roles_ { get; set; }

        public System.Data.Entity.DbSet<PAT.Models.DietRecommendation> DietRecommendations { get; set; }
    }
}