using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PAT.Models.Patient;

namespace PAT.Controllers
{
    public class TestDetailsController : Controller
    {
        private PatientDbContext db = new PatientDbContext();

        // GET: TestDetails
        public ActionResult Index()
        {
            var tests = db.Tests.Include(t => t.TestDetails_IN_PI).Include(t => t.TestDetails_IN_Test);
            return View(tests.ToList());
        }


        // GET: TestDetails/Create
        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "FirstName");
            ViewBag.DoctorID = new SelectList(db.DoctorDetails, "DoctorID", "FirstName");
            return View();
        }

        // POST: TestDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,DoctorID,PatientID,Test_TODO,TestPerformedDate,Test_Result,TestPrice,isActive")] TestDetails testDetails)
        {
            if (ModelState.IsValid)
            {
                db.Tests.Add(testDetails);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "FirstName", testDetails.PatientID);
            ViewBag.DoctorID = new SelectList(db.DoctorDetails, "DoctorID", "FirstName", testDetails.DoctorID);
            return View(testDetails);
        }

        // GET: TestDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestDetails testDetails = db.Tests.Find(id);
            if (testDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "FirstName", testDetails.PatientID);
            ViewBag.DoctorID = new SelectList(db.DoctorDetails, "DoctorID", "FirstName", testDetails.DoctorID);
            return View(testDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,DoctorID,PatientID,Test_TODO,TestPerformedDate,Test_Result,TestPrice,isActive")] TestDetails testDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "FirstName", testDetails.PatientID);
            ViewBag.DoctorID = new SelectList(db.DoctorDetails, "DoctorID", "FirstName", testDetails.DoctorID);
            return View(testDetails);
        }

        // GET: TestDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestDetails testDetails = db.Tests.Find(id);
            if (testDetails == null)
            {
                return HttpNotFound();
            }
            return View(testDetails);
        }

        // POST: TestDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestDetails testDetails = db.Tests.Find(id);
            db.Tests.Remove(testDetails);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        public ActionResult AddEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestDetails testDetails = db.Tests.Find(id);
            if (testDetails == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "FirstName", testDetails.PatientID);
            ViewBag.DoctorID = new SelectList(db.DoctorDetails, "DoctorID", "FirstName", testDetails.DoctorID);
            return View(testDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEdit([Bind(Include = "ID,DoctorID,PatientID,Test_TODO,TestPerformedDate,Test_Result,TestPrice,isActive")] TestDetails testDetails)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testDetails).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "FirstName", testDetails.PatientID);
            ViewBag.DoctorID = new SelectList(db.DoctorDetails, "DoctorID", "FirstName", testDetails.DoctorID);
            return View(testDetails);
        }



    }
}
