using FPTCourseManagement.Application.Module.Attendance.Queries;
using FPTCourseManagement.Application.Module.Courses.Commands;
using FPTCourseManagement.Application.Module.Attendance.Command;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Capstone.Api.Common.ResponseApi.Controllers;

namespace FPT_course_management.Module.Attendances.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : BaseController
    {

        private readonly IMediator _mediator;

        public AttendanceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPut]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> UpdateAttendance([FromBody] AttendanceCoomand list)
        {
            var result = await _mediator.Send(list);

            if (!result.IsSuccess)
                return ResponseBadRequest(result.Message);
            return ResponseNoContent();
        }

        //[HttpGet("GetList")]
        //[Authorize(Roles = "Teacher")]
        //public async Task<IActionResult> GetList(Guid cheduleId)
        //{
           
        //    var result = await _mediator.Send(new GetListAttendanceQuery() { ScheduleId = cheduleId } );
        //    if (!result.IsSuccess)
        //        return Ok(new ResponseAPI() { StatusCode = 400, Message = result.Message, Data = null });
        //    return Ok(new ResponseAPI() { StatusCode = 200, Message = result.Message, Data = result.Data });
        //}

        [HttpDelete("id")]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Delete(Guid id)
        {

            var result = await _mediator.Send(new DeleteAttendanceCommand() { AttendanceId = id });
            if (!result.IsSuccess)
                return ResponseBadRequest(result.Message);
            return ResponseNoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> Create(CreateAttendanceCommand model)
        {

            var result = await _mediator.Send(model);
            if (!result.IsSuccess)
                return ResponseBadRequest(result.Message);
            return ResponseOk(result.Data);
        }
    }
}
