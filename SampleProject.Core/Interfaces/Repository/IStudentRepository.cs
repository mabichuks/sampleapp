using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Core.Interfaces.Repository
{
    public interface IStudentRepository : IRepository<StudentModel>
    {
        // get students taking a particular course
        public IEnumerable<StudentModel> GetStudentsByCourse(int courseId);

        // get students by grade
    }
}
