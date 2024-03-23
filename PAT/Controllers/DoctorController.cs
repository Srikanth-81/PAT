using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Provider;
using PAT.Models;
using PAT.Models.Doctor;
using PAT.Models.Patient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PAT.Controllers
{
    public class DoctorController : Controller
    {
        // GET: Doctor
        [Authorize(Roles = "doctor")] 
        public ActionResult Index()
        {
            ViewData["uname"] = User.Identity.Name;
            return View();
        }

        [HttpGet]
        public ActionResult DoctorRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoctorRegister(DoctorDetails admin)
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
                admin.RoleID = 2;
                context.Doctors.Add(admin);
                context.SaveChanges();

                TempData["saved"] = "Doctor Details Added Successfully!";
                return RedirectToAction("DoctorsLogin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult DoctorsLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoctorsLogin(DoctorLogin mLogin)
        {
            if (ModelState.IsValid)
            {
                var context = new DoctorDbContext();
                var retrive = context.Doctors.Where(n => n.DoctorID == mLogin.DoctorID);
                foreach (var i in retrive)
                {
                    if (i.isApproved == false)
                    {
                        TempData["Message"] = "Admin approval is needed";
                        return View(mLogin);
                    }
                    if (i.DoctorID == mLogin.DoctorID && i.Password == mLogin.Password && i.isApproved == true)
                    {
                        FormsAuthentication.SetAuthCookie(mLogin.DoctorID, false);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Invalid Username or password";
                    }
                }
            }

            return View(mLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        
        }
       
        public ActionResult ViewPatients()
        {
            var patient = new PatientDbContext();
            var user = User.Identity.GetUserName();
            List<TestDetails> patientDiet = patient.Tests.ToList();

            return View(patientDiet);
        }


        [HttpGet]
        public ActionResult AddTest(int? id)
        {
            var db = new PatientDbContext();
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db1 = new DbContexts();
            TestDetails td = db1.Tests.Find(id);
            if(td == null)
            {
                return HttpNotFound();
            }
            return View(td);
        }


        [HttpPost]
        public ActionResult AddTest(TestDetails td)
        {
            var db = new DbContexts();
            if (ModelState.IsValid)
            {
                td.isActive = true;
                db.Entry(td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewPatients");
            }
            return View(td);
        }

        [HttpGet]
        public ActionResult AddResult(int? id)
        {
            var db = new PatientDbContext();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var db1 = new DbContexts();
            TestDetails td = db1.Tests.Find(id);
            if (td == null)
            {
                return HttpNotFound();
            }
            return View(td);
        }


        [HttpPost]
        public ActionResult AddResult(TestDetails td)
        {
            var db = new DbContexts();
            if (ModelState.IsValid)
            {
                td.isActive = true;
                db.Entry(td).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewPatients");
            }
            return View(td);
        }




        public ActionResult TestsDetails()
        {
            var user = new DbContexts();
            List<TestDetails> ts = user.Tests.ToList();
            return View(ts);
        }



    }
}