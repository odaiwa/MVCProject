using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
namespace MVCProject.Controllers
{
    public class UsersController : Controller
    {


        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        public ActionResult Create()
        {
            TempData["Roles"] = db.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();

            var names = new List<string>();
            names.Add("Admin");
            names.Add("Lecturer");
            names.Add("Student");
            TempData["asd"] = names as IEnumerable<SelectListItem>;

            string[] authors = { "Admin", "Lecturer",
                        "Student"};
            List<string> authorsRange = new List<string>(authors);

            TempData["123"] = authorsRange;
            TempData["odai"] = TempData["Roles"] as IEnumerable<SelectListItem>;
            return View();
        }

        [HttpPost]
        public ActionResult Create(User model1)
        {
            try
            {
                if (db.Users.Any(x => x.Username.Equals(model1.Username)))
                {
                    TempData["userMSG"] = "Choose another username";
                }
                 if (db.Users.Any(x => x.Email.Equals(model1.Email)))
                {
                    TempData["emailMSG"] = "Email Aleady Exist";
                }

                 if (db.Users.Any(x => x.Id.Equals(model1.Id)))
                {
                    TempData["idMSG"] = "ID Aleady Exist";
                }
                if (TempData["userMSG"] != null || TempData["emailMSG"] != null || TempData["idMSG"] != null)
                {
                    return View(model1);
                }
                else
                {
                    db.Users.Add(model1);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["exp"] = ex.ToString();
            }

            return View(model1);
        }
        public ActionResult Edit(int? id)
        {
            TempData["Roles"] = db.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User model)
        {
            try
            {
                var user = db.Users.First(x => x.Id == model.Id);
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Password = model.Password;
                user.Id = model.Id;
                user.Role = model.Role;
                user.Email = model.Email;
                user.Username = model.Username;
                if (db.Users.Any(x => x.Username.Equals(model.Username) && x.Id != model.Id))
                {
                    TempData["userMSG"] = "Choose another username";
                }
                if (db.Users.Any(x => x.Username.Equals(model.Username) && x.Id != model.Id))
                {
                    TempData["userMSG"] = "Choose another username";
                }
                if (db.Users.Any(x => x.Email.Equals(model.Email) && x.Id != model.Id))
                {
                    TempData["emailMSG"] = " Aleady Exists";
                }
                if ((db.Users.Any(x => x.Role.Equals("Admin"))) && (db.Users.Any(x => x.Role.Equals("Student"))) && (db.Users.Any(x => x.Role.Equals("Lecturer"))))
                {
                    TempData["userRole"] = "A role must be either Admin,Student or Lecturer";
                }

                if (TempData["userMSG"] != null || TempData["emailMSG"] != null || TempData["userRole"] != null)
                {
                    return View(model);
                }
                else
                {
                    if (model.Role.Equals("1"))
                    {
                        user.Role = "Admin";
                    }
                    else if (model.Role.Equals("2"))
                    {
                        user.Role = "Lecturer";
                    }
                    else if (model.Role.Equals("3"))
                    {
                        user.Role = "Student";
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["asd"] = ex.ToString();
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            User user1 = db.Users.Find(id);
            db.Users.Remove(user1);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}