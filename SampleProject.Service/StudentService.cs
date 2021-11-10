using SampleProject.Core.Dto;
using SampleProject.Core.Interfaces.Repository;
using SampleProject.Core.Interfaces.Services;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Service
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IEnrollmentRepository _enrollmentRepo;

        public StudentService(IStudentRepository studentRepo, ICourseRepository courseRepo, IEnrollmentRepository enrollmentRepo)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
            _enrollmentRepo = enrollmentRepo;
        }

        public async Task<ResponseModel<StudentDto>> AddStudent(StudentDto model)
        {
            var errors = new List<string>();
            try
            {
                var student = new StudentModel
                {
                    EnrolementDate = DateTime.Today,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = _studentRepo.Add(student);
                await _studentRepo.SaveChanges();
                model.Id = result.Id;
                model.EnrolementDate = student.EnrolementDate;

                return ResponseModel<StudentDto>.Succeeded(model);
            }
            catch (Exception ex)
            {
                errors.Add("Error occoured, unable to add student");
                return ResponseModel<StudentDto>.Failed("An error occured", ApiResponseCodes.INTERNAL_SERVER_ERROR, errors);
            }
        }

        public async Task<ResponseModel<int>> DeleteStudent(int id)
        {
            var errors = new List<string>();
            try
            {
                var student = await _studentRepo.Get(id);

                if(student == null)
                {
                    errors.Add("Id not found");
                    return ResponseModel<int>.Failed("Id not found", ApiResponseCodes.NOT_FOUND, errors);
                }

                _studentRepo.Remove(student);
                await _studentRepo.SaveChanges();
                return ResponseModel<int>.Succeeded(id);

            }
            catch (Exception ex)
            {
                errors.Add("Error occoured, unable to delete student");
                return ResponseModel<int>.Failed("An error occured", ApiResponseCodes.INTERNAL_SERVER_ERROR, errors);
            }
        }

        public async Task<ResponseModel<int>> EnrollStudent(int studentId, int courseId)
        {
            var errors = new List<string>();

            try
            {
                var student = await _studentRepo.Get(studentId);

                if (student == null)
                {
                    errors.Add("Student Id not found");
                    return ResponseModel<int>.Failed("Id not found", ApiResponseCodes.NOT_FOUND, errors);
                }

                var course  = await _courseRepo.Get(courseId);

                if (course == null)
                {
                    errors.Add("Course Id not found");
                    return ResponseModel<int>.Failed("Id not found", ApiResponseCodes.NOT_FOUND, errors);
                }

                var enrollment = new EnrollmentModel
                {
                    CourseId = courseId,
                    StudentId = studentId,

                };

                _enrollmentRepo.Add(enrollment);
                await _enrollmentRepo.SaveChanges();

                return ResponseModel<int>.Succeeded(enrollment.Id);



            }
            catch (Exception ex)
            {
                errors.Add("Error occoured, unable to enroll student");
                return ResponseModel<int>.Failed("An error occured", ApiResponseCodes.INTERNAL_SERVER_ERROR, errors);
            }
        }

        public async Task<ResponseModel<IEnumerable<StudentDto>>> GetStudentsByCourse(int courseId)
        {
            var errors = new List<string>();

            try
            {
                var course = await _courseRepo.Get(courseId);

                if (course == null)
                {
                    errors.Add("Course Id not found");
                    return ResponseModel<IEnumerable<StudentDto>>.Failed("Course Id not found", ApiResponseCodes.NOT_FOUND, errors);
                }

                var result = _studentRepo.GetStudentsByCourse(courseId);
                var dto = result.Select(x => new StudentDto
                {
                    EnrolementDate = x.EnrolementDate,
                    FirstName = x.FirstName,
                    Id = x.Id,
                    LastName = x.LastName

                });
                return ResponseModel<IEnumerable<StudentDto>>.Succeeded(dto);

            }
            catch (Exception ex)
            {
                errors.Add("Error occoured, unable to get students");
                return ResponseModel<IEnumerable<StudentDto>>.Failed("An error occured", ApiResponseCodes.INTERNAL_SERVER_ERROR, errors);
            }
        }

        public async Task<ResponseModel<StudentDto>> UpdateStudent(StudentDto model)
        {
            var errors = new List<string>();

            try
            {
                var student = await _studentRepo.Get(model.Id);

                if (student == null)
                {
                    errors.Add("Student Id not found");
                    return ResponseModel<StudentDto>.Failed("Id not found", ApiResponseCodes.NOT_FOUND, errors);
                }

                student.LastName = model.LastName;
                student.FirstName = model.FirstName;

                await _studentRepo.SaveChanges();

                return ResponseModel<StudentDto>.Succeeded(model);

            }
            catch (Exception)
            {
                errors.Add("Error occoured, unable to update student");
                return ResponseModel<StudentDto>.Failed("An error occured", ApiResponseCodes.INTERNAL_SERVER_ERROR, errors);
            }
        }
    }
}
