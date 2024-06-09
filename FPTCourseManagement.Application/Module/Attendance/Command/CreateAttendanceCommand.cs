using FPTCourseManagement.Application.Abtractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Attendance.Command
{
    public class CreateAttendanceCommand : IRequest<Result>
    {
        public  Guid StudentId { get; set; }
        public Guid ScheduleId { get; set; }
    }
}
