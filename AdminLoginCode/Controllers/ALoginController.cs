using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AdminLoginCode.Controllers
{
    public class ALoginController : Controller
    {
        private readonly static FinalCaseStudyEntities db = new FinalCaseStudyEntities();
        // GET: ALogin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Admin_Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult StudentLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Admin_Login(AdminLogin Objuser)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    using (FinalCaseStudyEntities db = new FinalCaseStudyEntities())
                    {

                        var obj = db.AdminLogins.Where(a => a.AdminId.Equals(Objuser.AdminId) && a.Password.Equals(Objuser.Password)).FirstOrDefault();
                        if (obj != null)
                        {
                            Session["AdminId"] = obj.AdminId.ToString();
                            Session["UserName"] = obj.UserName.ToString();
                            return RedirectToAction("Admin_DashBoard");
                        }
                        else
                        {
                            TempData["Success"] = "Id Or Password is Incorrect";
                            return View();
                        }
                    }
                }
                // return View(Objuser);
            }

            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            return View(Objuser);
        }
        public ActionResult Admin_DashBoard()
        {
            if (Session["AdminId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("AdminLoginpage");
            }

        }
        
        [HttpPost]
        public ActionResult StudentLogin(StudentDetail Objuser)
            {
                try
                {

                    if (ModelState.IsValid)
                    {
                        using (FinalCaseStudyEntities db = new FinalCaseStudyEntities())
                        {

                            var obj = db.StudentDetails.Where(a => a.Email.Equals(Objuser.Email) && a.Password.Equals(Objuser.Password)).FirstOrDefault();
                            if (obj != null)
                            {
                                Session["Email"] = obj.Email.ToString();
                              Session["StudentName"] = obj.StudentName.ToString();
                                return RedirectToAction("StudentDashBoard","Alogin");
                            }
                            else
                            {
                                TempData["Success"] = "Id Or Password is Incorrect";
                                return View();
                            }
                        }
                    }
                    // return View(Objuser);
                }

                catch (Exception ex)
                {
                    Response.Write(ex.ToString());
                }
                return View(Objuser);
            }
        public ActionResult StudentDashBoard()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(StudentDetail model)
        {
            if (ModelState.IsValid)

            {
                using (FinalCaseStudyEntities db = new FinalCaseStudyEntities())
                {
                    var checkUser = db.StudentDetails.Where(x => x.StudentName.Equals(model.StudentName)).FirstOrDefault();

                    if (checkUser == null)

                    {

                        // Add new user to the database

                        db.StudentDetails.Add(model);

                        db.SaveChanges();

                        return RedirectToAction("StudentLogin");

                    }

                    else
                    {
                        ViewBag.ErrorMessage = "User already exists";
                        return View();
                    }
                }
            }
            return View(model);
        }
        /* public ActionResult StudentDashBoard(string SearchId)
         {
             if (Session["StudentId"] != null)
             {
                 using (var db = new FinalCaseStudyEntities())
                 {
                     var users = db.StudentDetails.ToList();
                     if (!string.IsNullOrEmpty(SearchId))
                     {
                         users = users.Where(u => u.Email == Email).ToList();
                         *//*users = users.Where(u => u.StudentId.ToString() == SearchId).ToList();*//*
                         return View(users);
                     }
                     return View(users);
                 }
                 //return View();
             }
             else
             {
                 return RedirectToAction("StudentLogin");
             }
         }*/
        [HttpPost]
        public ActionResult Logout()
        {
            return RedirectToAction("ALogin", "AdminLogin");
        }
        [HttpPost]
        public ActionResult LogOut()
        {
            return RedirectToAction("Home", "HomePage");
        }


    }
}
