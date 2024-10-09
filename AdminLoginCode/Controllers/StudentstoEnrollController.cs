using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLoginCode.Controllers
{
    public class StudentstoEnrollController : Controller
    {
        public FinalCaseStudyEntities db = new FinalCaseStudyEntities();
            // GET: CoursesToView
            public ActionResult Index()
            {
                var Course = db.CourseDetails.ToList();
                return View(Course);

            }

            // GET: CourseDetails/Details/5
            public ActionResult Search()
            {
                return View();
            }
            [HttpPost]
            public ActionResult Search(int StudentId)
            {
                var Courses = db.StudentDetails.Where(b => b.StudentId == StudentId).ToList();

                if (Courses == null || !Courses.Any())
                {
                    ViewBag.Message = "No Id Found " + StudentId;
                }
                return View("Index", Courses);
            }

            // GET: StudentstoEnroll
        
    }
}