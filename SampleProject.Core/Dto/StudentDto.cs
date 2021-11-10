using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Core.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime EnrolementDate { get; set; }
    }
}
