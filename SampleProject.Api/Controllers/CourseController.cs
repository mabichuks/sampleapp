using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/course")]
    [ApiVersion("1")]
    public class CourseController : BaseApiController
    {

    }
}
