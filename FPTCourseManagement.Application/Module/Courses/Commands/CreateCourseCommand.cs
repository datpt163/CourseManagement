using FPTCourseManagement.Application.Abtractions.Messaging;
using FPTCourseManagement.Domain.Entities.Courses;
using MediatR;
using System;
using System.Collections.Generic;
namespace FPTCourseManagement.Application.Module.Courses.Commands
{
    public class CreateCourseCommand : IRequest<Result>
    {
        public string CourseCode { get; set; }
        public DateTime StartDate { get; set; }
        public Guid TypeSlotId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid TeacherId { get; set; }
    }
}
