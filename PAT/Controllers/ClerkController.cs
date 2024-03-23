using PAT.Models.Clerk;
using PAT.Models.Patient;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace PAT.Controllers
{
    public class ClerkController : Controller
    {
        // GET: Clerk
        [Authorize(Roles = "clerk")]
        public ActionResult Index()
        {
            ViewData["uname"] = User.Identity.Name;
            return View();
        }

        [HttpGet]
        public ActionResult ClerkRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClerkRegister(ClerkDetails admin)
        {
            if (ModelState.IsValid)
            {
                var context = new ClerkDbContext();
                var isUnique = context.Clerks.Where(n => n.ClerkID == admin.ClerkID);
                foreach (var i in isUnique)
                {
                    if (i.ClerkID == admin.ClerkID)
                    {
                        ViewData["Error"] = "ClerkID Already Exists";
                        return View(admin);
                    }
                }
                admin.RoleID = 3;
                context.Clerks.Add(admin);
                context.SaveChanges();

                TempData["saved"] = "Clerk Details Added Successfully!";
                return RedirectToAction("ClerksLogin");
            }
            return View();
        }

        [HttpGet]
        public ActionResult ClerksLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ClerksLogin(ClerkLogin mLogin)
        {
            if (ModelState.IsValid)
            {
                var context = new ClerkDbContext();
                var retrive = context.Clerks.Where(n => n.ClerkID == mLogin.ClerkID);
                foreach (var i in retrive)
                {
                    if (i.isApproved == false)
                    {
                        TempData["Message"] = "Admin approval is needed";
                        return View(mLogin);
                    }
                    if (i.ClerkID == mLogin.ClerkID && i.Password == mLogin.Password && i.isApproved == true)
                    {
                        FormsAuthentication.SetAuthCookie(mLogin.ClerkID, false);
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
        [Authorize(Roles = "clerk")]
        public ActionResult Patient()
        {
            var context = new PatientDbContext();
            List<PatientDetails> Patient = context.Patients.ToList();
            return View(Patient);
        }

        [Authorize(Roles = "clerk")]
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

        [Authorize(Roles = "clerk")]
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
        [Authorize(Roles = "clerk")]
        public ActionResult PatientEdit(int? id)
        {
            var db = new PatientDbContext();
            var model = db.Patients.FirstOrDefault(r => r.ID == id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [Authorize(Roles = "clerk")]
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult PatientEdit(PatientDetails patient)
        {
            var db = new PatientDbContext();
            var entry = db.Entry(patient);
            if (ModelState.IsValid)
            {
                entry.State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Patient", "Clerk");
            }
            return View(entry);
        }
        [HttpGet]
        [Authorize(Roles = "clerk")]
        public ActionResult CreatePatient()
        {
            return View();
        }

        // Admin Register after post request
        [HttpPost]
        [Authorize(Roles = "clerk")]
        public ActionResult CreatePatient(PatientDetails clerk)
        {
            if (ModelState.IsValid)
            {
                var context = new PatientDbContext();
                var isUnique = context.Patients.Where(n => n.PatientID == clerk.PatientID);
                foreach (var i in isUnique)
                {
                    if (i.PatientID == clerk.PatientID)
                    {
                        ViewData["Error"] = "Patient ID Already Exists";
                        return View(clerk);
                    }
                }
                context.Patients.Add(clerk);
                context.SaveChanges();
                TempData["saved"] = "Patient Details Added to Database Sucessfull!";
                return RedirectToAction("Patient");
            }
            return View();
        }
    }
}