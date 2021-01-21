using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class LogInController : Controller
    {
        private ApplicationDbContext _context = null;
        // GET: LogIn

        public LogInController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var id = int.Parse(User.Identity.Name);//taking the id of our user
                var user1 = _context.Users.First(x => x.Id == id);//taking the username from db and also checking if the id matches
                return RedirectToAction("LogInCheck",user1);
            }
            return View();
        }
        public ActionResult LogInCheck(User user1) {
            var LoggedInUser = _context.Users.FirstOrDefault(x => x.Username.Equals(user1.Username));//checks if the user we sent is in our data base
            if (LoggedInUser != null)
            {
                if (LoggedInUser.Password.Equals(user1.Password))
                {
                    Session["userId"] = LoggedInUser.Id;
                    Session["userFirst"] = LoggedInUser.FirstName;
                    Session["userName"] = LoggedInUser.Username;
                    Session["Lastname"] = LoggedInUser.LastName;


                    //Session["id"] = user1.Id;
                    if (LoggedInUser.Role.Equals("Admin"))
                    {
                        return RedirectToAction("AdminIndex", "Admin",LoggedInUser);
                    }
                    else if (LoggedInUser.Role.Equals("Student"))
                    {
                        return RedirectToAction("StudentIndex", "Student", LoggedInUser);
                    }
                    else if (LoggedInUser.Role.Equals("Lecturer"))
                    {
                        return RedirectToAction("LecturerIndex", "Lecturer", LoggedInUser);
                    }
                }
                else
                {
                    TempData["msg"] = "Password Incorrect";
                    return View();
                }
            }
            TempData["msg"] = "Username was not found";
            return View();


        }
    }
}