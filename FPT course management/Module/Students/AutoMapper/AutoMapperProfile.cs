using AutoMapper;
using FPTCourseManagement.Domain.Entities.Users;
using FPT_course_management.Module.Students.Controllers;
namespace FPT_course_management.Module.Students.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Student, responseStudent>();
        }
    }
}
