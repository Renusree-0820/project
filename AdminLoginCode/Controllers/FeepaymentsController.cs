using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdminLoginCode.Controllers
{
    public class FeepaymentsController : Controller
    {
        private readonly FinalCaseStudyEntities db = new FinalCaseStudyEntities();

        // GET: Feepayments
        public ActionResult Index()
        {
            var feepayments = db.Feepayments.Include(f => f.CourseDetail).Include(f => f.StudentDetail);
            return View(feepayments.ToList());
        }

        // GET: Feepayments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feepayment feepayment = db.Feepayments.Find(id);
            if (feepayment == null)
            {
                return HttpNotFound();
            }
            return View(feepayment);
        }

        // GET: Feepayments/Create
        public ActionResult Create()
        {
            ViewBag.Course_Id = new SelectList(db.CourseDetails, "Course_Id", "Course_Name");
            ViewBag.Student_Id = new SelectList(db.StudentDetails, "StudentId", "FirstName");
            var model = new Feepayment();
            model.Pending_Fee = model.CourseFee - model.Paid_Fee;
            return View(model);
            
        }

        // POST: Feepayments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentId,Student_Id,Course_Id,CourseFee,Course_Name,Paid_Fee,Pending_Fee,Payment_Date,Payment_Status")] Feepayment feepayment)
        {
            if (ModelState.IsValid)
            {
                db.Feepayments.Add(feepayment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Course_Id = new SelectList(db.CourseDetails, "Course_Id", "Course_Name", feepayment.Course_Id);
            ViewBag.Student_Id = new SelectList(db.StudentDetails, "StudentId", "FirstName", feepayment.Student_Id);
            return View(feepayment);
        }

        // GET: Feepayments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feepayment feepayment = db.Feepayments.Find(id);
            if (feepayment == null)
            {
                return HttpNotFound();
            }
            ViewBag.Course_Id = new SelectList(db.CourseDetails, "Course_Id", "Course_Name", feepayment.Course_Id);
            ViewBag.Student_Id = new SelectList(db.StudentDetails, "StudentId", "FirstName", feepayment.Student_Id);
            return View(feepayment);
        }

        // POST: Feepayments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,Student_Id,Course_Id,CourseFee,Course_Name,Paid_Fee,Pending_Fee,Payment_Date,Payment_Status")] Feepayment feepayment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feepayment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Course_Id = new SelectList(db.CourseDetails, "Course_Id", "Course_Name", feepayment.Course_Id);
            ViewBag.Student_Id = new SelectList(db.StudentDetails, "StudentId", "FirstName", feepayment.Student_Id);
            return View(feepayment);
        }

        // GET: Feepayments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feepayment feepayment = db.Feepayments.Find(id);
            if (feepayment == null)
            {
                return HttpNotFound();
            }
            return View(feepayment);
        }

        // POST: Feepayments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feepayment feepayment = db.Feepayments.Find(id);
            db.Feepayments.Remove(feepayment);
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

        public ActionResult DetailFeepayment()
        {
            try
            {
                using (var context = new FinalCaseStudyEntities())
                {
                    var Data = context.Feepayments.ToList();
                    return View(Data);
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return View();
        }
    }
}
