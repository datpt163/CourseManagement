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
    public class DeleteAttendanceCommandHandle : IRequestHandler<DeleteAttendanceCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAttendanceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DeleteAttendanceCommand request, CancellationToken cancellationToken)
        {
            var attendance = _unitOfWork.Attendances.FindByCondition(x => x.Id == request.AttendanceId).FirstOrDefault();
            if (attendance == null)
                return new Result(false, "Attendance not found", new object());
            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.SaveChanges();
            return new Result(true, "Delete success", new object());
        }
    }
}
