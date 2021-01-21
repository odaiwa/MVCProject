using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
using System.Net;
using MVCProject.LecturerViewModel;

namespace MVCProject.Controllers
{
    public class RegistrationController : Controller
    {
        private ApplicationDbContext _context = null;
        // GET: Registration

        public RegistrationController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Delete(int? idstudent,int? idcourse)
        {
            if (idstudent == null || idcourse == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            try
            {
            //find if this rows exists in the DB(idStudent && idCourse)
            Grade grade1 = _context.Grades.Find(_context.Grades.First(x => x.Courseid == idcourse && x.Studentid == idstudent));

            if (grade1 == null)
            {
                return HttpNotFound();
            }
            return View(grade1);
            }
            catch(Exception ex)
            {

            }
            return View();

        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DelteConfirmed(int idstudent, int idcourse)
        {
            //Cours course = _context.Grades.Find(id);
            _context.Grades.Remove(_context.Grades.First(x=>x.Courseid == idcourse && x.Studentid == idstudent));
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Index() => View(_context.Grades.ToList());

        public RegisterationViewModel GetRegisterationViewModel() => new RegisterationViewModel
        {
            courses = _context.Courses.ToList(),
            users = _context.Users.Where(x=>x.Role.Equals("Student")).ToList()
        };

        public ActionResult AddStudentToCourse() => View(GetRegisterationViewModel());

        [HttpPost]
        public ActionResult AddStudentToCourse(RegisterationViewModel model)
        {
            try
            {
                if (!(_context.Users.Where(x => x.Role.Equals("Student")).Any(x => x.Id.Equals(model.Grade.Studentid))))
                {
                    TempData["notFOUND"] = "This USER Doesn't Exist";
                }
                if (!(_context.Courses.Any(x => x.Courseid.Equals(model.Grade.Courseid))))
                {
                    TempData["coursenotFOUND"] = "This course not Exist";
                }
                if (_context.Grades.Any(x => x.Studentid.Equals(model.Grade.Studentid)))
                {
                    TempData["userMSG"] = "The student already Registerd to Course";
                }
                //if(_context.Courses.Any(x=>x.Day.Equals(model.courses1.Day) && x.TimeStart.Equals(model.courses1.TimeStart)))
                //{
                //    TempData["sametime"] = "This Student has course at the same time";
                //}
                if (TempData["notFound"] != null || TempData["userMSG"] != null || TempData["notFOUND1"] != null)
                {
                    var toReturn1 = GetRegisterationViewModel();
                    toReturn1.Grade = model.Grade;
                    return View(toReturn1);
                }
                else
                {
                    _context.Grades.Add(model.Grade);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
            }
            var toReturn = GetRegisterationViewModel();
            toReturn.Grade = model.Grade;
            return View(toReturn);
        }
    }
}