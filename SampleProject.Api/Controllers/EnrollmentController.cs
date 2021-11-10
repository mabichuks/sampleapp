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
    [Route("api/v{version:apiVersion}/enrollment")]
    [ApiVersion("1")]
    public class EnrollmentController : BaseApiController
    {
        private readonly IStudentService _studentService;

        public EnrollmentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost("")]
        [MapToApiVersion("1")]
        [ProducesResponseType(typeof(ResponseModel<int>), 200)]
        [ProducesResponseType(typeof(ResponseModel<int>), 500)]
        [ProducesResponseType(typeof(ResponseModel<int>), 404)]
        public async Task<IActionResult> Enroll(EnrollStudentDto dto)
        {
            var result = await _studentService.EnrollStudent(dto.CourseId, dto.StudentId);
            return ApiResponseWrapper(result);
        }
    }
}
