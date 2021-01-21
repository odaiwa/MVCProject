using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class StudentController : Controller
    {
        public ApplicationDbContext _context = null;
        // GET: Student
        public StudentController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Schedule()
        {
            List<int> user_courses = new List<int>();
            foreach (Grade g in _context.Grades)
                if (g.Studentid.Equals(Session["user"]))
                    user_courses.Add(g.Courseid);
            List<Cours> crs = new List<Cours>();
            foreach (int s in user_courses)
                crs.Add(_context.Courses.Find(s));
            return View(crs);

        }

        public ActionResult ExamSchedule()
        {
            try
            {

            List<int> user_courses = new List<int>();
            foreach (Grade g in _context.Grades)
                if (g.Studentid.Equals(Session["user"]))
                    user_courses.Add(g.Courseid);
            List<Cours> crs = new List<Cours>();
            foreach (int s in user_courses)
                crs.Add(_context.Courses.Find(s));
            return View(crs);
            }
            catch(Exception ex)
            {

            }
            return View();
        }

        public ActionResult StudentIndex(User user)
        {
            Session["user"] = user.Id;
            return View(user);
        }
    }
}