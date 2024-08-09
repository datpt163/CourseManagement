using FPTCourseManagement.Application.Abtractions.Messaging;
using FPTCourseManagement.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Attendance.Command
{
    public class AttendanceCoomand : IRequest<Result>
    {
        public List<SubCommandModel>? list { get; set; }
    }

    public class SubCommandModel
    {
        public Guid AttendanceId { get; set; }
        public AttendanceStatus Status { get; set; }
    }

}
