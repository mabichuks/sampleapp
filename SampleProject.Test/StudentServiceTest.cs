using Moq;
using SampleProject.Core.Dto;
using SampleProject.Core.Interfaces.Repository;
using SampleProject.Core.Interfaces.Services;
using SampleProject.Core.Models;
using SampleProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SampleProject.Test
{
    public class StudentServiceTest
    {
        private readonly Mock<IStudentRepository> _studentRepository = new Mock<IStudentRepository>();
        private readonly Mock<ICourseRepository> _courseRepo = new Mock<ICourseRepository>();
        private readonly Mock<IEnrollmentRepository> _enrollmentRepo = new Mock<IEnrollmentRepository>();
        private readonly IStudentService _studentService;


        public StudentServiceTest()
        {
            _studentService = new StudentService(_studentRepository.Object, _courseRepo.Object, _enrollmentRepo.Object);
        }

        [Fact]
        public async Task Add_Student_Should_Return_Todays_Date_And_Id()
        {
            //arrange
            var student = new Core.Dto.StudentDto
            {
                FirstName = "Mabi",
                LastName = "Chukwuma"
            };

            int id = 1;

            _studentRepository.Setup(x => x.Add(It.IsAny<StudentModel>())).Returns(new StudentModel 
            {
                Id = id,
                FirstName = "Mabi",
                LastName = "Chukwuma"
            });


            //act
            var response = await _studentService.AddStudent(student);

            //assert

            Assert.True(response.Data.EnrolementDate == DateTime.Today);
            Assert.True(response.Data.Id == id);
        }

        [Fact]
        public async Task Add_Student_Response_Has_Error_When_Exception_Thrown()
        {
            //arrange
            var student = new Core.Dto.StudentDto
            {
                FirstName = "Mabi",
                LastName = "Chukwuma"
            };

            _studentRepository.Setup(x => x.Add(It.IsAny<StudentModel>())).Throws(new Exception());


            //act
            var response = await _studentService.AddStudent(student);

            //assert

            Assert.True(response.HasError);
            Assert.True(response.Errors.Count > 0);
        }



        [Fact]
        public async Task Delete_Student_Returns_Error_When_Id_Not_Found()
        {
            //arrange

            _studentRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(() => null);


            //act
            var response = await _studentService.DeleteStudent(2);

            //assert

            Assert.Equal("Id not found", response.ResponseMessage);
            Assert.True(response.Errors.Count > 0);
            Assert.Equal(ApiResponseCodes.NOT_FOUND, response.ResponseCode);
        }

        [Fact]
        public async Task Delete_Student_Returns_Id_When_Succeeded()
        {
            //arrange

            _studentRepository.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(() => new StudentModel { 
                Id = 1,
                FirstName = "Chukwuma",
                LastName = "Mabi"
            });


            //act
            var response = await _studentService.DeleteStudent(1);

            //assert

            Assert.Equal(1, response.Data);
            Assert.True(response.Errors.Count == 0);
            Assert.Equal(ApiResponseCodes.OK, response.ResponseCode);
        }

        [Fact]
        public async Task Get_Student_By_Course_Returns_Error_When_Course_Not_Found()
        {
            //arrange

            var studentList = new List<StudentModel>
            {
                new StudentModel
                {
                     Id = 1,
                     FirstName = "Chukwuma",
                     LastName = "Mabi"
                },
                new StudentModel
                {
                     Id = 2,
                     FirstName = "Elvis",
                     LastName = "Okafor"
                },
                new StudentModel
                {
                     Id = 3,
                     FirstName = "Badore",
                     LastName = "Nnaji"
                }
            };

            _courseRepo.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(() => null);

            _studentRepository.Setup(x => x.GetStudentsByCourse(It.IsAny<int>())).Returns(studentList);


            //act
            var response = await _studentService.GetStudentsByCourse(1);

            //assert

            Assert.Equal("Course Id not found", response.ResponseMessage);
            Assert.True(response.Errors.Count > 0);
            Assert.Equal(ApiResponseCodes.NOT_FOUND, response.ResponseCode);
        }

        [Fact]
        public async Task Get_Student_By_Course_Returns_Success_When_Course_Is_Found()
        {
            //arrange

            var studentList = new List<StudentModel>
            {
                new StudentModel
                {
                     Id = 1,
                     FirstName = "Chukwuma",
                     LastName = "Mabi"
                },
                new StudentModel
                {
                     Id = 2,
                     FirstName = "Elvis",
                     LastName = "Okafor"
                },
                new StudentModel
                {
                     Id = 3,
                     FirstName = "Badore",
                     LastName = "Nnaji"
                }
            };

            var expected = studentList.Select(x => new StudentDto
            {
                EnrolementDate = x.EnrolementDate,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName
            });

            _courseRepo.Setup(x => x.Get(It.IsAny<int>())).ReturnsAsync(() => new CourseModel());

            _studentRepository.Setup(x => x.GetStudentsByCourse(It.IsAny<int>())).Returns(studentList);

            //act
            var response = await _studentService.GetStudentsByCourse(1);

            //assert

            Assert.NotNull(response.Data);
            Assert.True(response.Errors.Count == 0);
            Assert.Equal(ApiResponseCodes.OK, response.ResponseCode);
        }
    }
}
