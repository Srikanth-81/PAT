using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using PAT.Models;

namespace PAT.Controllers
{
    [Authorize(Roles = "doctor")]
    public class DietRecommendationsController : Controller
    {
        private DbContexts db = new DbContexts();

        // GET: DietRecommendations
        public ActionResult Index()
        {
            var dietRecommendations = db.DietRecommendations.Include(d => d.DoctorID_IN_Diet).Include(d => d.PatientID_IN_Diet);
            return View(dietRecommendations.ToList());
        }

        // GET: DietRecommendations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietRecommendation dietRecommendation = db.DietRecommendations.Find(id);
            if (dietRecommendation == null)
            {
                return HttpNotFound();
            }
            return View(dietRecommendation);
        }

        // GET: DietRecommendations/Create
        public ActionResult Create()
        {
            var user = User.Identity.GetUserName();
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "FirstName");
            ViewBag.PatientId = new SelectList(db.Patients.Where(n => n.DoctorID == user.ToString()), "PatientID", "FirstName");
            return View();
        }

        // POST: DietRecommendations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DietID,PatientId,DoctorId,DietDuration,DietContent,RecommendedExercise")] DietRecommendation dietRecommendation)
        {
            if (ModelState.IsValid)
            {
                db.DietRecommendations.Add(dietRecommendation);
                dietRecommendation.DoctorId = User.Identity.GetUserName();
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "FirstName", dietRecommendation.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientID", "FirstName", dietRecommendation.PatientId);
            return View(dietRecommendation);
        }

        // GET: DietRecommendations/Edit/5
        public ActionResult Edit(int? id)
        {
            var user = User.Identity.GetUserName();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietRecommendation dietRecommendation = db.DietRecommendations.Find(id);
            if (dietRecommendation == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "FirstName", dietRecommendation.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients.Where(n => n.DoctorID == user.ToString()), "PatientID", "FirstName", dietRecommendation.PatientId);
            return View(dietRecommendation);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DietID,PatientId,DoctorId,DietDuration,DietContent,RecommendedExercise")] DietRecommendation dietRecommendation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dietRecommendation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorID", "FirstName", dietRecommendation.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientID", "FirstName", dietRecommendation.PatientId);
            return View(dietRecommendation);
        }

        // GET: DietRecommendations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DietRecommendation dietRecommendation = db.DietRecommendations.Find(id);
            if (dietRecommendation == null)
            {
                return HttpNotFound();
            }
            return View(dietRecommendation);
        }

        // POST: DietRecommendations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DietRecommendation dietRecommendation = db.DietRecommendations.Find(id);
            db.DietRecommendations.Remove(dietRecommendation);
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
    }
}
