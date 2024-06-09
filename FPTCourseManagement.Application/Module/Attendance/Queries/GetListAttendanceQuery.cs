using FPTCourseManagement.Application.Abtractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Attendance.Queries
{
    public class GetListAttendanceQuery : IRequest<Result>
    {
        public Guid ScheduleId { get; set; }
    }
}
