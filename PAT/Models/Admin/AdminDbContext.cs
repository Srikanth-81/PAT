using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PAT.Models.Admin
{
    public class AdminDbContext : DbContext
    {
        public AdminDbContext() : base("PatientTracker")
        {
        }
        public DbSet<AdminDetails> Admin { get; set; }

        public DbSet<AdminLogin> AdminLogins { get; set; }
    }
}