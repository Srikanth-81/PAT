using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PAT.Models.Doctor
{
    public class DoctorDbContext : DbContext
    {
        public DoctorDbContext() : base("PatientTracker")
        {
        }
        public DbSet<DoctorDetails> Doctors { get; set; }

        public DbSet<DoctorLogin> DoctorLogins { get; set; }
    }
}