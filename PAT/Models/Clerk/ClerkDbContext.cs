using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PAT.Models.Clerk
{
    public class ClerkDbContext:DbContext
    {
        public ClerkDbContext() : base("PatientTracker")
        {
        }
        public DbSet<ClerkDetails> Clerks { get; set; }

        public DbSet<ClerkLogin> ClerkLogins { get; set; }

        public System.Data.Entity.DbSet<PAT.Models.Patient.PatientDetails> PatientDetails { get; set; }

        public System.Data.Entity.DbSet<PAT.Models.Roles_> Roles_ { get; set; }
    }
}