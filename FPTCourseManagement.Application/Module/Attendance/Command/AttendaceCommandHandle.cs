using FPTCourseManagement.Application.Abtractions.Messaging;
using FPTCourseManagement.Application.Module.Attendance.Queries;
using FPTCourseManagement.Domain.Enums;
using FPTCourseManagement.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Attendance.Command
{
    public class AttendaceCommandHandle : IRequestHandler<AttendanceCoomand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendaceCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(AttendanceCoomand request, CancellationToken token)
        {
            if (request.list == null || request.list.Count == 0)
                return new Result(false, "List null or empty", new object());

            foreach (var item in request.list)
            {
                var temp = _unitOfWork.Attendances.FindByCondition(x => x.Id == item.AttendanceId).FirstOrDefault();
                if (temp != null && (item.Status == AttendanceStatus.Absent || item.Status == AttendanceStatus.Attend))
                {
                    temp.Status = item.Status;
                    _unitOfWork.Attendances.Update(temp);
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return new Result(true, "Success", new object());
        }
    }
}
