using FPTCourseManagement.Application.Abtractions.Messaging;
using FPTCourseManagement.Application.Module.Courses.Commands;
using FPTCourseManagement.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Attendance.Queries
{
    public class GetListAttendanceQueryHandle : IRequestHandler<GetListAttendanceQuery, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListAttendanceQueryHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(GetListAttendanceQuery request, CancellationToken token)
        {

            var sche = _unitOfWork.Schedules.FindByCondition(c => c.Id == request.ScheduleId).Include(c => c.Attendances).ThenInclude(s => s.Student).FirstOrDefault();

            if (sche == null)
                return new Result(false, "Schedule not found", new object());
            return new Result(true, "Success", sche.Attendances);
        }
    }
}
