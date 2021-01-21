namespace MVCProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        [Required]
        public string NameCourse { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Courseid { get; set; }


        public string LecturerName { get; set; }

        [Required]
        public string Day { get; set; }

        public TimeSpan TimeStart { get; set; }

        public TimeSpan TimeEnd { get; set; }

        public DateTime DateMoedA { get; set; }

        public DateTime DateMoedB { get; set; }
        public int? lecturerID { get; set; }
    }
}
