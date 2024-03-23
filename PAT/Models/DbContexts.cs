using PAT.Models.Admin;
using PAT.Models.Clerk;
using PAT.Models.Doctor;
using PAT.Models.Patient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PAT.Models
{
    public class DbContexts: DbContext
    {
        public DbContexts(): base("PatientTracker")
        {

        }
        public DbSet<ClerkDetails> Clerks { get; set; }
        public DbSet<ClerkLogin> ClerkLogins { get; set; }
        public DbSet<AdminDetails> Admin { get; set; }
        public DbSet<AdminLogin> AdminLogins { get; set; }
        public DbSet<DoctorDetails> Doctors { get; set; }
        public DbSet<DoctorLogin> DoctorLogins { get; set; }
        public DbSet<PatientDetails> Patients { get; set; }
        public DbSet<PatientLogin> PLogins { get; set; }
        public DbSet<Roles_> roles { get; set; }
        public DbSet<DietRecommendation> DietRecommendations { get; set; }
        public DbSet<TestDetails> Tests { get; set; }

        public int MyProperty { get; set; }
    }
}