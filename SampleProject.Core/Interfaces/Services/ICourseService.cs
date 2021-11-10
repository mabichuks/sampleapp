using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Core.Interfaces.Services
{
    public interface ICourseService
    {
        Task<CourseModel> AddCourse(CourseModel course);
        Task<CourseModel> UpdateCourse(CourseModel course);
    }
}
