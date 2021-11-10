using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Core.Models
{
    public class StudentModel : BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrolementDate { get; set; }
    }
}
