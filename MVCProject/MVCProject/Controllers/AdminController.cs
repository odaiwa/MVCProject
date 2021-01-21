using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
namespace MVCProject.Controllers
{
    public class AdminController : Controller
    {
        public ApplicationDbContext _context = new ApplicationDbContext();
        
        public ActionResult AdminIndex(User user)
        {
            Session["user"] = user.Id;
            return View(user);
        }
    }
}