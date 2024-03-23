using PAT.Models.Admin;
using PAT.Models.Doctor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PAT.Models.Patient;
using PAT.Models.Clerk;
using PAT.Models;

namespace PAT.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize(Roles = "admin")]

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }

        // Admin Login after post request
        [HttpPost]
        public ActionResult AdminLogin(AdminLogin aLogin)
        {
            if (ModelState.IsValid)
            {
                var context = new AdminDbContext();
                var retrive = context.Admin.Where(n => n.AdminID == aLogin.AdminID);
                foreach (var i in retrive)
                {
                    if (i.AdminID == aLogin.AdminID && i.Password == aLogin.Password && i.isApproved == false)
                    {
                        FormsAuthentication.SetAuthCookie(aLogin.AdminID, false);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Error"] = "Invalid Username or password";
                    }
                }
            }
            TempData["Error"] = "Invalid Username or password";
            return View(aLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult AdminRegister()
        {
            return View();
        }

        // Admin Register after post request
        [HttpPost]
        public ActionResult AdminRegister(AdminDetails admin)
        {
            if (ModelState.IsValid)
            {
                var context = new AdminDbContext();
                var isUnique = context.Admin.Where(n => n.AdminID == admin.AdminID);
                foreach (var i in isUnique)
                {
                    if (i.AdminID == admin.AdminID)
                    {
                        ViewData["Error"] = "AdminID Already Exists";
                        return View(admin);
                    }
                }
                admin.RoleID = 1;
                context.Admin.Add(admin);
                context.SaveChanges();
                TempData["saved"] = "Admin Details Added to Database Sucessfull!";
                return RedirectToAction("AdminLogin");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Doctors()
        {
            var context = new DoctorDbContext();
            List<DoctorDetails> doctors = context.Doctors.ToList();
            return View(doctors);
        }

        [Authorize(Roles = "admin")]
        public ActionResult DoctorApproved(int id)
        {
            var context = new DoctorDbContext();
            var user = context.Doctors.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isApproved = true;
                context.SaveChanges();
            }

            return RedirectToAction("Doctors");
        }
        [Authorize(Roles = "admin")]
        public ActionResult DoctorRejected(int id)
        {
            var context = new DoctorDbContext();
            var user = context.Doctors.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isApproved = false;
                context.SaveChanges();
            }

            return RedirectToAction("Doctors");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult DoctorEdit(int? id)
        {
            var db = new DoctorDbContext();
            var model = db.Doctors.FirstOrDefault(r => r.ID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult DoctorEdit(DoctorDetails doctor)
        {
            var db = new DoctorDbContext();
            var entry = db.Entry(doctor);
            if (ModelState.IsValid)
            {
                entry.State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Doctors", "Admin");
            }
            return View(entry);
        }
        // Doctor Creation
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateDoctor()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreateDoctor(DoctorDetails admin)
        {
            if (ModelState.IsValid)
            {
                var context = new DoctorDbContext();
                var isUnique = context.Doctors.Where(n => n.DoctorID == admin.DoctorID);
                foreach (var i in isUnique)
                {
                    if (i.DoctorID == admin.DoctorID)
                    {
                        ViewData["Error"] = "Doctor ID Already Exists";
                        return View(admin);
                    }
                }
                context.Doctors.Add(admin);
                context.SaveChanges();
                TempData["saved"] = "Doctor Details Added to Database Sucessfull!";
                return RedirectToAction("Doctors");
            }
            return View();
        }

        [Authorize(Roles = "admin")]
        public ActionResult Patient()
        {
            var context = new PatientDbContext();
            List<PatientDetails> Patient = context.Patients.ToList();
            return View(Patient);
        }

        [Authorize(Roles = "admin")]
        public ActionResult PatientApproved(int id)
        {
            var context = new PatientDbContext();
            var user = context.Patients.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isApproved = true;
                context.SaveChanges();
            }
            List<PatientDetails> patient = context.Patients.Where(n => n.isApproved == true).ToList();
            return RedirectToAction("Patient");
        }

        [Authorize(Roles = "admin")]
        public ActionResult PatientRejected(int id)
        {
            var context = new PatientDbContext();
            var user = context.Patients.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isApproved = false;
                context.SaveChanges();
            }
            List<PatientDetails> patient = context.Patients.Where(n => n.isApproved == true).ToList();
            return RedirectToAction("Patient");
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
       // [Authorize(Roles = "patient")]
        public ActionResult PatientEdit(int? id)
        {
            var db = new PatientDbContext();
            var model = db.Patients.FirstOrDefault(r => r.ID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [Authorize(Roles = "admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PatientEdit(PatientDetails patient)
        {
            var db = new PatientDbContext();
            var entry = db.Entry(patient);
            if (ModelState.IsValid)
            {
                entry.State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Patient", "Admin");
            }
            return View(entry);
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreatePatient()
        {
            return View();
        }

        // Admin Register after post request
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult CreatePatient(PatientDetails admin)
        {
            if (ModelState.IsValid)
            {
                var context = new PatientDbContext();
                var isUnique = context.Patients.Where(n => n.PatientID == admin.PatientID);
                foreach (var i in isUnique)
                {
                    if (i.PatientID == admin.PatientID)
                    {
                        ViewData["Error"] = "Patient ID Already Exists";
                        return View(admin);
                    }
                }
                context.Patients.Add(admin);
                context.SaveChanges();
                TempData["saved"] = "Patient Details Added to Database Sucessfull!";
                return RedirectToAction("Patient");
            }
            return View();
        }
        [Authorize(Roles = "admin")]
        public ActionResult Clerks()
        {
            var context = new ClerkDbContext();
            List<ClerkDetails> clerk = context.Clerks.ToList();
            return View(clerk);
        }

        [Authorize(Roles = "admin")]
        public ActionResult ClerkApproved(int id)
        {
            var context = new ClerkDbContext();
            var user = context.Clerks.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isApproved = true;
                context.SaveChanges();
            }
           
            return RedirectToAction("Clerks");
        }

        [Authorize(Roles = "admin")]
        public ActionResult ClerkRejected(int id)
        {
            var context = new ClerkDbContext();
            var user = context.Clerks.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.isApproved = false;
                context.SaveChanges();
            }
            
            return RedirectToAction("Clerks");
        }


        // Clerk Creation
        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult CreateClerk()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult CreateClerk(ClerkDetails admin)
        {
            if (ModelState.IsValid)
            {
                var context = new ClerkDbContext();
                var isUnique = context.Clerks.Where(n => n.ClerkID == admin.ClerkID);
                foreach (var i in isUnique)
                {
                    if (i.ClerkID == admin.ClerkID)
                    {
                        ViewData["Error"] = "Clerk ID Already Exists";
                        return View(admin);
                    }
                }
                context.Clerks.Add(admin);
                context.SaveChanges();
                TempData["saved"] = "Clerk Details Added to Database Sucessfull!";
                return RedirectToAction("Clerks");
            }
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult  ClerkEdit(int? id)
        {
            var db = new ClerkDbContext();
            var model = db.Clerks.FirstOrDefault(r => r.ID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public ActionResult ClerkEdit(ClerkDetails clerk)
        {
            var db = new ClerkDbContext();
            var entry = db.Entry(clerk);
            if (ModelState.IsValid)
            {
                entry.State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Clerks", "Admin");
            }
            return View(entry);
        }


        //[HttpGet]
        //public ActionResult TestData()
        //{
        //    DbContexts db = new DbContexts(); 
        //    ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorID");
        //    ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientID");
        //    return View();
        //}

        //[HttpPost, ValidateAntiForgeryToken]
        //public ActionResult TestData(TestDetails testdata)
        //{
        //    DbContexts db = new DbContexts();
        //    ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorID");
        //    ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "patientID");
        //    db.Entry(testdata).State = EntityState.Added;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}



    }
}