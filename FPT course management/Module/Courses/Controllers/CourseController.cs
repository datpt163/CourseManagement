using FPTCourseManagement.Domain.Entities.Courses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FPTCourseManagement.Application.Module.Courses.Commands;
using Microsoft.AspNetCore.Authorization;
using FPTCourseManagement.Domain.Repository;
using Capstone.Api.Common.ResponseApi.Controllers;
namespace FPT_course_management.Module.Courses.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : BaseController
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
                return ResponseOk(result.Message);
            return ResponseOk(result.Data);
        }

        [HttpPost("add-student-into-course")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> AddStudentIntoCourse([FromBody] AddStudentIntoCourseHandle list)
        {
            var result = await _mediator.Send(list);

            if (!result.IsSuccess)
                return ResponseBadRequest(result.Message);
            return ResponseOk(result.Data);
        }
    }
}
