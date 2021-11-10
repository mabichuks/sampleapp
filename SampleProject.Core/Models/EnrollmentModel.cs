using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Core.Models
{
    public class EnrollmentModel : BaseModel
    {
        public int CourseId { get; set; }
        public CourseModel Course { get; set; }
        public int StudentId { get; set; }
        public StudentModel Student { get; set; }
        public string Grade { get; set; }
        public int Score { get; set; }
    }
}
