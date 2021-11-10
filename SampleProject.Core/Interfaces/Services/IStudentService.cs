using SampleProject.Core.Dto;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Core.Interfaces.Services
{
    public interface IStudentService
    {
        Task<ResponseModel<StudentDto>> AddStudent(StudentDto model);
        Task<ResponseModel<StudentDto>> UpdateStudent(StudentDto model);
        Task<ResponseModel<int>> DeleteStudent(int id);
        Task<ResponseModel<int>> EnrollStudent(int studentId, int courseId);
        Task<ResponseModel<IEnumerable<StudentDto>>> GetStudentsByCourse(int courseId);
    }
}
