using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    [Table("DaysOfWeek")]
    public class DaysOfWeek
    {
        [Key]
        public string Name { get; set; }
    }
}