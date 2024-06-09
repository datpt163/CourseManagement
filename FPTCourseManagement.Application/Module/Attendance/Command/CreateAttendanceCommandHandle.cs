using FPTCourseManagement.Application.Abtractions.Messaging;
using FPTCourseManagement.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Attendance.Command
{
    public class CreateAttendanceCommandHandle : IRequestHandler<CreateAttendanceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateAttendanceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var schedule = _unitOfWork.Schedules.FindByCondition(x => x.Id == request.ScheduleId).FirstOrDefault();
            var student = _unitOfWork.Students.FindByCondition(x => x.AccountId == request.StudentId).FirstOrDefault();
            if (schedule == null)
                return new Result(false, "schedule not found", new object());
            if (student == null)
                return new Result(false, "student not found", new object());
            _unitOfWork.Attendances.Add(new FPTCourseManagement.Domain.Entities.Schedules.Attendance() {  Id = Guid.NewGuid(), ScheduleId = request.ScheduleId, StudentId = request.StudentId });
            _unitOfWork.SaveChanges();
            return new Result(true, "Add success", new object());
        }
    }
}
