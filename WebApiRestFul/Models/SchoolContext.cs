using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCityManager.Models
{
    public class SchoolContext : DbContext
    { 
        public SchoolContext() : base()
        {
        }

        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { set; get; }
        public DbSet<Enrollement> Enrollements{ set; get;}
        public DbSet<Student> Students {set; get;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Enrollement>().ToTable("Enrollement");
        }
    }
}
