using FPTCourseManagement.Application.Abtractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Courses.Commands
{
    public class AddStudentIntoCourseHandle : IRequest<Result>
    {
        public List<AddStudentIntoCourseModel>? list { get; set; }
    }

    public class AddStudentIntoCourseModel
    {
        public string? CourseCode { get; set; }
        public string? StudentCode { get; set; }
    }
}
