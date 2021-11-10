using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Core.Models
{
    public class CourseModel : BaseModel
    {
        public string Title { get; set; }
        public  string Description { get; set; }
        public  int Credits { get; set; }
    }
}
