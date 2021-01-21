using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
using MVCProject.LecturerViewModel;

namespace MVCProject.Controllers
{
    public class LecturerController : Controller
    {
        // GET: Lecturer
        public ApplicationDbContext _context = null;
        // GET: Student
        public LecturerController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult LecturerIndex(User user)
        {
            Session["user"] = user.Id;
            return View(user);
        }

        public ActionResult ShowStudents()
        {
            List<Cours> lecCourses = new List<Cours>();
            foreach (Cours c in _context.Courses)
                if (c.lecturerID.Equals(Session["user"]))
                    lecCourses.Add(c);
            return View(lecCourses);
        }
        public ActionResult StudentsTable(int courseID)
        {
            List<int> studentsid = new List<int>();
            List<User> students = new List<User>();
            foreach (Grade g in _context.Grades)
                if (g.Courseid.Equals(courseID))
                    studentsid.Add(g.Studentid);
            foreach (int s in studentsid)
                students.Add(_context.Users.Find(s));
            return View(students);
        }
        public ActionResult EditGrades(RegisterationViewModel model)
        {
            var model1 = GetUserViewModel();
            model1.students = model.students;
            return View(model1);
        }
        public RegisterationViewModel GetUserViewModel() => new RegisterationViewModel
        {
            students = _context.Users.ToList()
        };
    }
}