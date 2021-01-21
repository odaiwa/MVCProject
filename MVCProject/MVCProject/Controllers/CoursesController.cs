using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVCProject.LecturerViewModel;

namespace MVCProject.Controllers
{

    public class CoursesController : Controller
    {
        // GET: Courses
        private ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index() => View(_context.Courses);

        public ActionResult Create()
        {
            return View(GetCourseViewModel());
        }


        [HttpPost]
        public ActionResult Create(RegisterationViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = _context.Users.First(x => x.Id == model.courses1.lecturerID);
                model.courses1.LecturerName = user.FirstName +" "+ user.LastName;
                _context.Courses.Add(model.courses1);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            var toReturn1 = GetCourseViewModel();
            toReturn1.courses1 = model.courses1;

            return View(toReturn1);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cours course = _context.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }

            var model = GetCourseViewModel();
            model.courses1 = course;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(RegisterationViewModel model)
        {
            try
            {
                var user = _context.Courses.First(x => x.Courseid == model.courses1.Courseid);
                user.DateMoedA = model.courses1.DateMoedA;
                user.DateMoedB = model.courses1.DateMoedB;
                user.Day = model.courses1.Day;
                user.lecturerID = model.courses1.lecturerID;
                //user.LecturerName = _context.Users.Where(x => x.Id.Equals(model.courses1.lecturerID)).Select(x => x.FirstName).ToString();
                user.NameCourse = model.courses1.NameCourse;
                user.TimeEnd = model.courses1.TimeEnd;
                user.TimeStart = model.courses1.TimeStart;
                //user.RoleID = model.user1.RoleID;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                var model1 = GetCourseViewModel();
                model1.user12 = model.user12;
                return View(model1);
            }
        }
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours course = _context.Courses.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DelteConfirmed(int id)
        {
            Cours course = _context.Courses.Find(id);
            _context.Courses.Remove(course);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public RegisterationViewModel GetCourseViewModel() => new RegisterationViewModel
        {
            user12 = _context.Users.Where(x => x.Role.Equals("Lecturer")).Select(x => new SelectListItem
            {
                Text = x.Username,
                Value = x.Id.ToString()
            }).ToList(),
            days = _context.Days.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            }).ToList(),
            users = _context.Users.Where(x=>x.Role.Equals("Lecturer")).ToList()

        };


    }
}