namespace MVCProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Collections.Generic;

    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Cours> Courses { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Grade> Grades { get; set; }
        public virtual DbSet<DaysOfWeek> Days { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
