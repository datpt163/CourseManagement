using FPTCourseManagement.Domain.Entities.Courses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FPT_course_management.Common.Controllers;
using FPTCourseManagement.Application.Module.Courses.Commands;
using Microsoft.AspNetCore.Authorization;
namespace FPT_course_management.Module.Courses.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseCommand course)
        {
            var result = await _mediator.Send(course);

            if(!result.IsSuccess) 
                return Ok(new ResponseAPI() { StatusCode = 400, Message = result.Message, Data = null } );
            return Ok(new ResponseAPI() { StatusCode = 200, Message = result.Message, Data = result.Data });
        }

        [HttpPost("AddStudentIntoCourse")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddStudentIntoCourse([FromBody] AddStudentIntoCourseHandle list)
        {
            var result = await _mediator.Send(list);

            if (!result.IsSuccess)
                return Ok(new ResponseAPI() { StatusCode = 400, Message = result.Message, Data = null });
            return Ok(new ResponseAPI() { StatusCode = 200, Message = result.Message, Data = result.Data });
        }
    }
}
