using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
namespace MVCProject.LecturerViewModel
{
    public class LecturerView
    {
        public User user { get; set; }
        public List<User> Names { get; set; }
        public Cours course { get; set; }
    }
}