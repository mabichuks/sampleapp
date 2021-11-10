using Microsoft.AspNetCore.Mvc;
using SampleProject.Core.Dto;
using SampleProject.Core.Interfaces.Services;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/student")]
    [ApiVersion("1")]
    public class StudentController : BaseApiController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("")]
        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ResponseModel<StudentDto>), 200)]
        [ProducesResponseType(typeof(ResponseModel<StudentDto>), 500)]
        public async Task<IActionResult> Create(StudentDto student)
        {
            var result = await _studentService.AddStudent(student);
            return ApiResponseWrapper(result);
        }

        [HttpGet("{courseId}")]
        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 500)]
        [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 404)]
        public async Task<IActionResult> GetStudentByCourse(int courseId)
        {
            var result = await _studentService.GetStudentsByCourse(courseId);
            return ApiResponseWrapper(result);
        }

        [HttpPut("")]
        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 200)]
        [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 500)]
        [ProducesResponseType(typeof(ResponseModel<IEnumerable<StudentDto>>), 404)]
        public async Task<IActionResult> GetStudentByCourse(StudentDto student)
        {
            var result = await _studentService.UpdateStudent(student);
            return ApiResponseWrapper(result);
        }
    }
}
