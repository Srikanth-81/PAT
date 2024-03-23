using Microsoft.AspNet.Identity;
using PAT.Models;
using PAT.Models.Admin;
using PAT.Models.Doctor;
using PAT.Models.Patient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PAT.Controllers
{
    public class PatientController : Controller
    {
        // GET: Patient
        [Authorize(Roles = "patient")]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Patients()
        {
            var db = new DbContexts();
            var user = User.Identity.GetUserName();
            List<TestDetails> patient = db.Tests.Where(n => n.PatientID == user).ToList();
            return View(patient);
        }




 
        public ActionResult TestApproved(int id)
        {
            var context = new PatientDbContext();
            var db = new DbContexts();

            var user = context.Patients.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.TestRequest = true;
                context.SaveChanges();
            }

            return RedirectToAction("Patients");
        }

        public ActionResult TestRejected(int id)
        {
            var context = new PatientDbContext();
            var user = context.Patients.Where(n => n.ID == id).ToList();
            foreach (var i in user)
            {
                i.TestRequest = false;
                context.SaveChanges();
            }

            return RedirectToAction("Patients");
        }







        public ActionResult VIewTestResults()
        {
            var patient = new PatientDbContext();
            var user = User.Identity.GetUserName();
            List<TestDetails> patientDiet = patient.Tests.Where(n => n.PatientID == user.ToString()).ToList();
            return View(patientDiet);
        }

        public ActionResult DietRec()
        {
            var patient = new DbContexts();
            var user = User.Identity.GetUserName();
            List<DietRecommendation> patientDiet = patient.DietRecommendations.Where(n => n.PatientId == user.ToString()).ToList();

            return View(patientDiet);

        }


        [HttpGet]
        public ActionResult PatientRegister()
        {
            var context = new PatientDbContext();
            DoctorDbContext db = new DoctorDbContext();
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorID");
            return View();
        }

        [HttpPost]
        public ActionResult PatientRegister(PatientDetails admin)
        {
            var patient = new PatientDetails
            {
                PatientID = admin.PatientID,
                Password = admin.Password,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Age = admin.Age,
                Gender = admin.Gender,
                RoleID = admin.RoleID,
                ContectNumber = admin.ContectNumber,
                DoctorID = admin.DoctorID,
                TestRequest = admin.TestRequest,
                Illnes = admin.Illnes,
                isApproved = admin.isApproved
            };

            if (ModelState.IsValid)
            {
                var context = new PatientDbContext();
                var isUnique = context.Patients.Where(n => n.PatientID == admin.PatientID);
                DoctorDbContext db = new DoctorDbContext();
                ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorID");

                foreach (var i in isUnique)
                {
                    if (i.PatientID == admin.PatientID)
                    {
                        ViewData["Error"]="Patient ID Already Exists";
                        return View(admin);
                    }
                }

                admin.RoleID = 4;
                context.Patients.Add(admin);
                context.SaveChanges();


                var testdetails = new TestDetails()
                {
                    DoctorID = admin.DoctorID,
                    PatientID = admin.PatientID,
                    Test_TODO = null,
                    TestPerformedDate = null,
                    Test_Result = null,
                    TestPrice = null,
                    isActive = false
                };

                var pils = new PatientDbContext();
                pils.Tests.Add(testdetails);
                pils.SaveChanges();

                TempData["saved"] = "Patient Details Added Successfully!";
                return RedirectToAction("PatientLogin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult PatientLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PatientLogin(PatientLogin pLogin)
        {
            if (ModelState.IsValid)
            {
                var context = new PatientDbContext();
                var retrive = context.Patients.Where(n => n.PatientID == pLogin.PatientID);
                foreach (var i in retrive)
                {
                    if (i.isApproved == false)
                    {
                        TempData["Message"] = "Admin approval is needed";
                        return View(pLogin);
                    }
                    if (i.PatientID == pLogin.PatientID && i.Password == pLogin.Password && i.isApproved == true)
                    {
                        FormsAuthentication.SetAuthCookie(pLogin.PatientID, false);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["Error"] = "Invalid Username or password";
                    }

                }
            }
            return View(pLogin);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return Redirect("/Home/Index");
        }
    }
}