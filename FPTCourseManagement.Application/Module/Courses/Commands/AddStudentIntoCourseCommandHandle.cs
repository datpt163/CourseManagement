using FPTCourseManagement.Application.Abtractions.Messaging;
using FPTCourseManagement.Domain.Entities.Courses;
using FPTCourseManagement.Domain.Enums;
using FPTCourseManagement.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Courses.Commands
{
    public class AddStudentIntoCourseCommandHandle : IRequestHandler<AddStudentIntoCourseHandle, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AddStudentIntoCourseCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AddStudentIntoCourseHandle request, CancellationToken token)
        {
            List<CourseStudent> list = new List<CourseStudent>();
            if (request.list is null || request.list.Count == 0)
                return new Result(false, "List is null or empty", list);

            foreach (var v in request.list)
            {
                var course = _unitOfWork.Courses.FindByCondition(x => x.CourseCode == v.CourseCode).FirstOrDefault();
                if (course != null)
                {
                    var student = _unitOfWork.Students.FindByCondition(x => x.StudentCode == v.StudentCode).FirstOrDefault();
                    if (student != null)
                    {
                        var stucourse = _unitOfWork.CourseStudents.FindByCondition(x => x.StudentId == student.AccountId && x.CourseId == course.Id).FirstOrDefault();
                        var stucourse2 = list.FirstOrDefault(x => x.StudentId == student.AccountId && x.CourseId == course.Id);

                        if (stucourse == null && stucourse2 == null)
                            list.Add(new CourseStudent() { Id = Guid.NewGuid(), StudentId = student.AccountId, CourseId = course.Id });
                    }
                }
            }

            _unitOfWork.CourseStudents.AddRange(list);
            foreach (var n in list)
            {
                var temp = _unitOfWork.Schedules.FindByCondition(x => x.CourseId == n.CourseId).ToList();
                if (temp != null)
                {
                    foreach (var c in temp)
                    {
                        _unitOfWork.Attendances.Add(new Domain.Entities.Schedules.Attendance() { ScheduleId = c.Id, Status = AttendanceStatus.Absent, StudentId = n.StudentId });
                    }
                }

            }
            await _unitOfWork.SaveChangesAsync();

            return new Result(true, "Success", list);
        }
    }
}
