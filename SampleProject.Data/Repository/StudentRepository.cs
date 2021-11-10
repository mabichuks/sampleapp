using SampleProject.Core.Interfaces.Repository;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SampleProject.Data.Repository
{
    public class StudentRepository : Repository<StudentModel>, IStudentRepository
    {
        public StudentDbContext Context => _dbContext as StudentDbContext;
        public StudentRepository(StudentDbContext context) : base(context)
        {

        }
        public IEnumerable<StudentModel> GetStudentsByCourse(int courseId)
        {
            var result = from e in Context.Enrollments
                         join s in Context.Students on e.StudentId equals s.Id
                         join c in Context.Courses on e.CourseId equals c.Id
                         where c.Id == courseId
                         select new StudentModel
                         {
                             EnrolementDate = s.EnrolementDate,
                             FirstName = s.FirstName,
                             Id = s.Id,
                             LastName = s.LastName
                         };
            return result.ToList();

        }
    }
}
