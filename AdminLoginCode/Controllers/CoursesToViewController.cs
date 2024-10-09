using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AdminLoginCode.Controllers
{
    public class CoursesToViewController : Controller
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
        public ActionResult Search(string Course_Name)
        {
            var Courses = db.CourseDetails.Where(b => b.Course_Name == Course_Name).ToList();

            if (Courses == null || !Courses.Any())
            {
                ViewBag.Message = "No flights found between " + Course_Name;
            }
            return View("Index", Courses);
        }
        


    }

}