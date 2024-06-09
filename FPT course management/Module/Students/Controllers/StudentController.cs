using AutoMapper;
using FPTCourseManagement.Domain.Entities.Courses;
using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FPT_course_management.Module.Students.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        public StudentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Teacher")]
        public IActionResult getList()
        {
            var x = unitOfWork.Students.FindAll().ToList();
            List<responseStudent> temp = new List<responseStudent>();
            foreach(var item in x)
            {
                responseStudent destination = _mapper.Map<Student, responseStudent>(item);
                temp.Add(destination);
            }
            return Ok(temp);
        }
    }

    public class responseStudent {
        public string StudentCode { get; private set; }
    }
}
