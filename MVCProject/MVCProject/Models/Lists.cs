using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Models
{

    public class Lists
    {
        ApplicationDbContext db = new ApplicationDbContext();
        
        public List<SelectListItem> lects { get; set; }

    }
}