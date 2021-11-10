using Microsoft.EntityFrameworkCore;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Data
{
    public class StudentDbContext : DbContext
    {
        public DbSet<StudentModel> Students { get; set; }
        public DbSet<CourseModel> Courses { get; set; }
        public DbSet<EnrollmentModel> Enrollments { get; set; }

        public StudentDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
