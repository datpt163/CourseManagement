using FPTCourseManagement.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace FPT_course_management.Module.Schedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ScheduleController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Teacher")]
        public IActionResult Getlist()
        {
            var x = _unitOfWork.Schedules.FindAll().Include(c => c.Course).Include(c => c.Teacher).Include(c => c.TimeSlot).ToList();

            if (x is null || x.Count() == 0)
                return Ok("Schedule is empty");

            List<ResponGetList> list = new List<ResponGetList>();
            foreach(var n in x)
            {
                list.Add(new ResponGetList() { Id = n.Id , Slot = n.TimeSlot.Name, CourseCode = n.Course.CourseCode, Date = n.DateOfLessson} );
            }
            return Ok(list);
        }
    }

    public class ResponGetList
    {
        public Guid Id { get; set; }
        public string? Slot { get; set; }
        public string? CourseCode { get; set; }
        public DateTime Date { get; set; }
    }
}
