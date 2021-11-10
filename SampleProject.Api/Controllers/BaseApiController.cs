using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.Api.Controllers
{
    public class BaseApiController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        protected IActionResult ApiResponseWrapper<T>(ResponseModel<T> response)
        {
            return ReturnHttpMessage(response.ResponseCode, response);
        }
        private IActionResult ReturnHttpMessage<T>(ApiResponseCodes code, ResponseModel<T> response)
        {
            switch (code)
            {
                case ApiResponseCodes.BAD_REQUEST:
                    return this.StatusCode(StatusCodes.Status400BadRequest, response);
                case ApiResponseCodes.INTERNAL_SERVER_ERROR:
                    return this.StatusCode(StatusCodes.Status500InternalServerError, response);
                case ApiResponseCodes.NOT_FOUND:
                    return this.StatusCode(StatusCodes.Status404NotFound, response);
                case ApiResponseCodes.OK:
                default:
                    return Ok(response);
            }
        }
    }
}
