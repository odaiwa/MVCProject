using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
namespace MVCProject.LecturerViewModel
{
    public class RegisterationViewModel
    {

        public Grade Grade { get; set; }
        public Cours courses1 { get; set; }
        public User users1 { get; set; }
        public List<SelectListItem> days { get; set; }
        public List<User> users { get; set; }
        public List<SelectListItem> user12 { get; set; }
        public List<Cours> courses { get; set; }
        public List<User> students { get; set; }
        public List<Grade> studentsgrade { get; set; }


    }
}